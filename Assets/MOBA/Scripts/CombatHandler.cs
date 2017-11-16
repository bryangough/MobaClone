using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Networking;

public class CombatHandler : NetworkBehaviour 
{
	public Team team;
	public LayerMask myTeamLayerMask;
	public LayerMask otherTeamLayerMask;
	TargetableObject _targetedObject;

	public delegate void TargetChanged();
    public event TargetChanged targetChanged;
	public bool isActive = true;
	public TurretHandler turretHandler;

	public bool controlTurret = false;
	PowerHandler powerHandler;
	// Use this for initialization
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
			otherTeamLayerMask = LayerMask.GetMask("LeftSide");
			this.gameObject.layer = LayerMask.NameToLayer("RightSide");
			
		}
		else if(team == Team.Left)
		{
			myTeamLayerMask = LayerMask.GetMask("LeftSide");
			otherTeamLayerMask = LayerMask.GetMask("RightSide");
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
		if( target != null )	
		{
			if( target.team != team )
			{
				if( powerHandler.isPowerReady(0) )
				{
					//powerHandler.usePower(0);
					CmdUsePower(0, target.gameObject);
				}
				//attach target with basic shot.
				//powerHandler.
			}
			
		}
	}
	[Command]
	void CmdUsePower(int powerId, GameObject targetGameObject)
	{
		BasicPower power = powerHandler.getPower(powerId);
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
						target.health.takeDamage(power.dmg);
						usedPower = true;
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
	//these only happen on users. Not on server.
	[ClientRpc]
	void RpcUsePower(int powerId)
	{
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
}
/*


   */