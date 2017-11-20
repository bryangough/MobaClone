using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TurretHandler : MonoBehaviour {
	// Use this for initialization
	CombatHandler combatHandler;
	Animator animator;
	//public Transform turret;
	public float offset = 90f;
	public Transform parent;
	void Start () {
		
		parent = this.gameObject.transform.parent;
		combatHandler = this.GetComponentInParent<CombatHandler>();
		combatHandler.turretHandler = this;
		animator = this.GetComponent<Animator>();
	}
	public void fireCannon()
	{
		animator.SetTrigger("fire");
	}
	public void stopFiring()
	{
	}
	
	// Update is called once per frame
	void Update () {
		if( !combatHandler.controlTurret )
		{
			return;
		}
		//center
		if( combatHandler.target == null)
		{
			if( transform.rotation != parent.rotation )
			{
				transform.rotation = parent.rotation;
			}
		}
		else
		{
			Transform target = combatHandler.target.transform;
			var delta = target.position - parent.transform.position;
 			var angle = Mathf.Atan2(delta.y, delta.x) * Mathf.Rad2Deg;
			 angle += offset;
 			var rotation = Quaternion.Euler(0, 0, angle);
			transform.rotation = rotation;
		}

	}
	//move to target
	//move to center
}
