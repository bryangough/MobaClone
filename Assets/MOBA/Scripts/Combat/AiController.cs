using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Networking;
public class AiController : NetworkBehaviour {

	public float targetRange = 1f;

	TargetFinder targetFinder;
	CombatHandler combatHandler;
	public MovementHandler movementHandler;
	
	public bool isBusy = false;

	public float delayCounter = 0;
	public float checkDelay = 1f;
	// Use this for initialization
	[Server]
	void Start () {
		if( !isServer )
		{
			return;
		}
		combatHandler = this.GetComponent<CombatHandler>();
		combatHandler.targetChanged += targetChanged;
		targetFinder = this.GetComponent<TargetFinder>();
		movementHandler = this.GetComponent<MovementHandler>();
		
	}
	[Server]
	void Update () 
	{
		if( !isServer )
		{
			return;
		}
		target = combatHandler.target;
		delayCounter += Time.deltaTime;
		//I'll establish a queue system so AI actions are not run every frame and can be spread out a bit
		// For now I'll have it check every second
		if(delayCounter > checkDelay)
		{
			delayCounter = 0;
			doChecks();	
		}
	}
	void targetChanged()
	{
		delayCounter = checkDelay;
		if( combatHandler.target==null || !combatHandler.target.isAlive())
		{
			combatHandler.target = null;
			targetFinder.getNewTarget();
		}
	}
    void OnDrawGizmosSelected()
    {
        // Display the explosion radius when selected
        Gizmos.color = new Color(1, 1, 0, 0.75F);
        Gizmos.DrawWireSphere(transform.position, targetRange);
    }
    public TargetableObject target;
	// Update is called once per frame
	
//if isbusy - wait until attack is done or timer is done?
	
	public void doChecks()
	{
		if(combatHandler.target != null)
		{
			//is target is dead, get new target
			if( !combatHandler.target.isAlive() )
			{
				combatHandler.target = null;
				targetFinder.getNewTarget();
				if(movementHandler!=null)
				{
					movementHandler.keepMoving();
				}
				return;
			}
			if( !isBusy )
			{

				bool inRange = combatHandler.testDistanceToTarget(targetRange);
				if( inRange )
				{
					bool hasLineOfSite = combatHandler.testLineOfSightEnemy();
					if (hasLineOfSite)
					{
						 //RaycastHit2D
						 // attack target
						 //Debug.Log("attack target.");
						 //isBusy = true;
						 //use power
						 if(movementHandler!=null)
						 	movementHandler.stopMoving();
					}
					else
					{
						//get new target?
						Debug.Log(this.name +" has target blocked. "+combatHandler.target.name );
					}
				}
				else
				{
					combatHandler.target = null;
					targetFinder.getNewTarget();
					if(movementHandler!=null)
					{
						movementHandler.keepMoving();
					}
				}
			}
			//is currently doing action
			//is target in range
			//is target in view
			
			//combatHandler.
			//if( targetRange)
		}
		else if( movementHandler!=null)
		{
			if( movementHandler.currentState == MOVEMENT_STATE.IDLE )
			{
				movementHandler.keepMoving();
			}
		}

		//or become dead
		/*if(combatHandler.target == null)
		{
			targetFinder.getNewTarget();
		}*/


		//if target moves out of targetRange
	}
	
}
