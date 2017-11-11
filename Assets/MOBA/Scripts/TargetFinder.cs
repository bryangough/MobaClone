using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Networking;

public class TargetFinder : NetworkBehaviour {
	public float radius = 1f;
	public bool searchForTarget = true;
	public float checkDelay = 5;
	public float delayCounter = 0;
	public LayerMask layerMask;
	CombatHandler combatHandler;
	void Start()
	{
		//layerMask = 1 << LayerMask.NameToLayer("LeftSide");
		combatHandler = this.GetComponent<CombatHandler>();
	}
	// Update is called once per frame
	void Update () 
	{
		if( !isServer )
		{
			return;
		}
		if(	searchForTarget )
		{
			delayCounter += Time.deltaTime;

			if(delayCounter > checkDelay)
			{
				//will add priority system later
				Collider2D[] colliders = Physics2D.OverlapCircleAll(this.transform.position, radius, layerMask);
				/*for (int x=0; x<colliders.Length; x++)
				{
					//colliders
				}*/
				if( colliders.Length>0 )
				{
					combatHandler.target = colliders[0].GetComponent<TargetableObject>();
					searchForTarget = false;
					delayCounter = 0;
				}
			}
		}
		//
	}
	public void getNewTarget()
	{
		searchForTarget = true;
		delayCounter = checkDelay;
	}
}
