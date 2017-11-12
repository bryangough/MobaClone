using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CombatHandler : MonoBehaviour 
{
	public Team team;
	public LayerMask myTeamLayerMask;
	public LayerMask otherTeamLayerMask;
	TargetableObject _targetedObject;

	public delegate void TargetChanged();
    public event TargetChanged targetChanged;
	public bool isActive = true;
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
			//attach target with basic shot.
		}
	}
}
/*

[Command]
  void CmdFire()
  {
    // Create the Bullet from the Bullet Prefab
    var bullet = (GameObject)Instantiate(
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
    Destroy(bullet, 2.0f);
  }
   */