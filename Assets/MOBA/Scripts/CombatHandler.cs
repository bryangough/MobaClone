using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Networking;

public class CombatHandler : NetworkBehaviour 
{
	public Team team;
	public LayerMask myTeamLayerMask;
	public LayerMask otherTeamLayerMask;
	//
	

	public delegate void TargetChanged();
    public event TargetChanged targetChanged;
	[SyncVar(hook = "OnDeactivate")]
	public bool isActive = true;
	//
	public TurretHandler turretHandler;
	public GameObject firePoint;
	//
	public bool controlTurret = false;
	PowerHandler powerHandler;
	//
	TargetableObject _targetedObject;
	[SerializeField]
	public TargetableObject target
	{
		get { 
			return _targetedObject; 
		}
		set { 
			if( _targetedObject != value )
			{
				_targetedObject = value;
				if( targetChanged != null)
				{
					targetChanged();
				}
			}
		}
	}
	//public TargetableObject target;
	void Start () {
		if(team == Team.Right)
		{
			myTeamLayerMask = LayerMask.GetMask("RightSide");
			otherTeamLayerMask = LayerMask.GetMask("LeftSide", "Neutral");
			this.gameObject.layer = LayerMask.NameToLayer("RightSide");	
		}
		else if(team == Team.Left)
		{
			myTeamLayerMask = LayerMask.GetMask("LeftSide");
			otherTeamLayerMask = LayerMask.GetMask("RightSide", "Neutral");
			this.gameObject.layer = LayerMask.NameToLayer("LeftSide");
		}
		else//Neutral enenimes
		{
			myTeamLayerMask = LayerMask.GetMask("Neutral");
			otherTeamLayerMask = LayerMask.GetMask("RightSide", "LeftSide");
			this.gameObject.layer = LayerMask.NameToLayer("Neutral");
		}
		controlTurret = isServer || isLocalPlayer;

		powerHandler = this.GetComponent<PowerHandler>();
		//this.gameObject.layer = 1 << myTeamLayerMask.value;
	}

	void OnDrawGizmos() 
	{
		if(target!=null)
		{
			Gizmos.DrawLine(transform.position, target.transform.position);
		}
	}
	
	// Update is called once per frame
	void Update () 
	{
		if( !isServer && !isLocalPlayer)
		{
			return;
		}
//		Debug.Log(isServer+" "+name+" "+target+" "+powerHandler.isPowerReady(0));
		//this shouldn't happen every frame
		if( target != null && powerHandler.isPowerReady(0) )	
		{
			usePower(0, true);
		}
	}
	//should maybe change this to not be so many ifs
	public void displayError(string errorMessage, bool surpressError)
	{
		if(!surpressError)
		{
			Debug.Log(this.name + " had error: " + errorMessage);
		}
	}
	public bool usePower(int powerId)
	{
		return usePower(powerId, false);
	}
	public bool usePower(int powerId, bool surpressError)
	{
		BasicPower power = powerHandler.getPower(powerId);
		if( power == null)
		{
			displayError("Power not found.", surpressError);
			return false;
		}
		
		if( !powerHandler.isPowerReady(powerId) && powerId>0 )
		{
			displayError("Power not ready.", surpressError);
			return false;
		}
		
		if( power.requiresTarget && target == null )	
		{
			displayError("Power requires target.", surpressError);
			return false;
		}
		
		if( !power.alwaysTargetSelf )
		{
			
			if( power.targetType == TARGET_TYPE.OTHERTEAM && target.team == team )
			{
				displayError("Power target not other team.", surpressError);
				return false;
			}		
			else if( power.targetType == TARGET_TYPE.MYTEAM && target.team != team )
			{
				displayError("Power target MY team.", surpressError);
				return false;
			}
			
			if(	!testDistanceToTarget(power.range) )
			{
				displayError("Power target not in range.", surpressError);
				return false;
			}
			
			if(	power.requiresLineOfSite && !testLineOfSightEnemy() )
			{
				displayError("Can't see target.", surpressError);
				return false;
			}
			
			CmdUsePower(powerId, target.gameObject, this.gameObject);
		}	
		else
		{
			CmdUsePower(powerId, this.gameObject, this.gameObject);
		}
		
		return true;
	}
	// Checks
	public bool testDistanceToTarget(float targetRange)
	{
		Vector3 offset = target.transform.position - transform.position;
		float sqrLen = offset.sqrMagnitude;
		
		if( sqrLen < targetRange * targetRange)
		{
			return true;
		}
		return false;
	}
	public bool testLineOfSightFriend()
	{
		return testLineOfSight(myTeamLayerMask);
	}
	public bool testLineOfSightEnemy()
	{
		return testLineOfSight(otherTeamLayerMask);
	}
	public bool testLineOfSight(LayerMask layerMask)
	{
		RaycastHit2D[] hits = Physics2D.LinecastAll(transform.position, target.transform.position, otherTeamLayerMask.value);
		for( int x=0;x<hits.Length;x++)
		{
			if (hits[x].transform == target.transform)
			{
				return true;
			}
		}
		return false;
	}
	public void didLastHit(GameObject attacker)
	{

	}
	[Command]
	void CmdUsePower(int powerId, GameObject targetGameObject, GameObject attacker)
	{
		BasicPower power = powerHandler.getPower(powerId);
		//Debug.Log(this.name + " use power: " + power.weaponname);
		//target
		bool usedPower = true;
		if( power!=null	)
		{
			TargetableObject target = null;
			if( targetGameObject!=null)
			{
				target = targetGameObject.GetComponent<TargetableObject>();
				if( target != null )
				{
					if( target.isAlive() )
					{
						//May need to account for lag here.
						//Test vs location on timestamp. For now leave it.
						//if(	testDistanceToTarget(power.range) && testLineOfSightEnemy() )
						//{
							//check within range?
							//even for instant, should I make it an object that is applied next frame?
							if(power.isInstant)
							{
								target.health.takeDamage(power.dmg, attacker);
								usedPower = true;
							}
							else
							{
								var bullet = (GameObject)Instantiate(
									power.bullet,
									firePoint.transform.position,
									firePoint.transform.rotation);

								MovementHandler movementHandler = bullet.GetComponent<MovementHandler>();
								Bullet bulletHandler = bullet.GetComponent<Bullet>();
								bulletHandler.initialize( Object.Instantiate(power) as BasicPower, target, attacker );
								
								if( movementHandler!= null )
								{
									movementHandler.moveToTarget(target.transform, bulletHandler.deliverPayload);
									NetworkServer.Spawn(bullet);
								}								
								//if(!isLocalPlayer)
								//bullet.GetComponent<MeshRenderer>().material.color = Color.blue;
							}
						//}
						
					}
				}
			}
			if(usedPower)
			{
				powerHandler.usePower(powerId);
				RpcUsePower(powerId);
			}
			//takeDamage
		}
		//check if power off cooldown?
		//apply 

		// Create the Bullet from the Bullet Prefab
		/*var bullet = (GameObject)Instantiate(
			bulletPrefab,
			bulletSpawn.position,
			bulletSpawn.rotation);

		// Add velocity to the bullet
		bullet.GetComponent<Rigidbody>().velocity = bullet.transform.forward * 6;

		if(isLocalPlayer)
		bullet.GetComponent<MeshRenderer>().material.color = Color.blue;

		// Spawn the bullet on the Clients
		NetworkServer.Spawn(bullet);

		// Destroy the bullet after 2 seconds
		Destroy(bullet, 2.0f);*/
	}
	
	[ClientRpc]
	void RpcUsePower(int powerId)
	{
//		Debug.Log("RpcUsePower "+isServer);
		if (isLocalPlayer)
		{
			powerHandler.usePower(powerId);
			//return to spawn, start repawn timer
			// move back to zero location
			//transform.position = Vector3.zero;
		}
		if(turretHandler!=null)
		{
			turretHandler.fireCannon();
		}
	}
	void OnDeactivate(bool isActive)
	{
		this.gameObject.SetActive(isActive);
	}

	
}
/*


   */