using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TurretHandler : MonoBehaviour {
	// Use this for initialization
	CombatHandler combatHandler;
	public Transform turret;
	public float offset = 90f;
	//public GameObject parent;
	void Start () {
		combatHandler = this.GetComponent<CombatHandler>();
		//parent = this.gameObject.transform.parent;
	}
	
	// Update is called once per frame
	void Update () {
		
		//center
		if( combatHandler.target == null)
		{
			if( transform.rotation != Quaternion.identity )
			{
				transform.rotation = Quaternion.identity;
			}
		}
		else
		{
			Transform target = combatHandler.target.transform;
			var delta = target.position - this.transform.position;
 			var angle = Mathf.Atan2(delta.y, delta.x) * Mathf.Rad2Deg;
			 angle += offset;
 			var rotation = Quaternion.Euler(0, 0, angle);
			turret.rotation = rotation;
		}

	}
	//move to target
	//move to center
}
