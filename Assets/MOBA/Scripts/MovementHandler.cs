using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Networking;

public class MovementHandler : NetworkBehaviour {

	bool isAlive = true;
	protected float stateTimer = 0.25f;
	protected float stateTimerTime = 0.25f;
	public MOVEMENT_STATE currentState = MOVEMENT_STATE.IDLE;
	//Animator animator;
	//public Transform myTransform;
	protected Vector3 targetLocation;
	public Transform target;
	public Rigidbody2D rigidbody;
	public bool useRigidbody = true;
	public float moveSpeed = 0.1f; //move speed
	public float distanceFromTarget = 0.2f;
	protected Vector2 facing;
	
	public bool updateOnClient = false;

	public delegate void MoveCallback();
    MoveCallback moveCallback;
	// Use this for initialization
	void Start () {
		doStart();
	}
	protected void doStart()
	{
		rigidbody = this.GetComponent<Rigidbody2D>();
		if( rigidbody == null)
		{
			useRigidbody = false;
		}
		if( !isServer )
			return;
	}
	// Update is called once per frame
	void Update () {
		if( !updateOnClient )
		{
			if( !isServer && !isLocalPlayer )
				return;
		}
		
		doUpdate();
	}
	protected void doUpdate () 
	{
		if(this.isAlive)
		{
			stateTimer -= Time.deltaTime;
			stateCheck();
		}
	}

	


	public void moveToTarget(Transform target, MoveCallback callback)
	{
		currentState = MOVEMENT_STATE.TARGET;
		this.target = target;
		moveCallback = callback;
		doFacing(this.target.position);
	}
	public void moveToLocation(Vector3 targetLocation, MoveCallback callback)
	{
		moveCallback = callback;
		moveToLocation( targetLocation );
	}
	public void moveToLocation(Vector3 targetLocation)
	{
		currentState = MOVEMENT_STATE.LOCATION;
		this.targetLocation = targetLocation;
		this.target = null;
		doFacing(this.targetLocation);
		//float rot_z = Mathf.Atan2(dir.y, dir.x) * Mathf.Rad2Deg;
        //transform.rotation = Quaternion.Euler(0f, 0f, rot_z - 90);
		//facing = transform.TransformDirection (-Vector3.forward);

//		Debug.Log(facing);
	}
	void doFacing(Vector3 targetLocation)
	{
		facing = targetLocation - transform.position;
		
		facing.Normalize();
		//facing.z = 0;
		rotateBody();
	}
	public void stopMoving()
	{
		currentState = MOVEMENT_STATE.IDLE;
	}
	public virtual void keepMoving()
	{

	}
	public void rotateBody()
	{
		if( useRigidbody )
		{
			//SignedAngle gives the proper rotation.
			float angle = Vector3.SignedAngle(Vector3.down,  facing, Vector3.forward);
			if(rigidbody != null)
			{
				rigidbody.rotation = angle;
				rigidbody.angularVelocity = 0;
			}
		}
		else
		{
			transform.rotation = Quaternion.LookRotation(-Vector3.forward,  facing);
		}
	}
	
	protected void stateCheck()
	{
		if(currentState==MOVEMENT_STATE.TARGET)
		{
			if( target==null )
			{
				endMovement();
			}
			//re-adjust direction
			if (stateTimer<=0)
			{
				doFacing(target.position);
				stateTimer = stateTimerTime;
			}
				if(useRigidbody)
				{
					rigidbody.MovePosition( rigidbody.position + facing * moveSpeed * Time.deltaTime );
					rigidbody.angularVelocity = 0;
				}
				else
				{
					transform.position += (Vector3)(facing * moveSpeed * Time.deltaTime);
				}
				
				Vector3 dist = target.position-transform.position;
				dist.z = 0;
				if(dist.sqrMagnitude<distanceFromTarget*distanceFromTarget)
				{
					endMovement();
				}

				/*
				facing = transform.position - target.position;
				facing.z = 0;
				//too far
				if(facing.sqrMagnitude>=distanceFromTarget*distanceFromTarget)
				{
					//callback 
					//enterWander();
					return;
				}
				//too close
				if(facing.sqrMagnitude<distanceFromTarget*distanceFromTarget)
				{
					currentState = MOVEMENT_STATE.IDLE;
					//getWanderSpot();//callback 
				}
				//myTransform.rotation = Quaternion.LookRotation(Vector3.forward,  dir);
				facing.Normalize();
				//should still get this to work, or remove rigid bodies and do everything with code?
				//rigidbody2D.AddForce(-dir * chaseSpeed * Time.deltaTime);
				transform.position += facing * moveSpeed * Time.deltaTime;
				rotateBody(); */
			//}
			//else
			//{
				//enterWander();//callback 
			//}
		}
		else if(currentState==MOVEMENT_STATE.LOCATION)
		{
			if (stateTimer<=0)
			{
				doFacing(targetLocation);
				stateTimer = stateTimerTime;
			}
			//if ( stateTimer>0 )
			//{
				if(useRigidbody)
				{
					rigidbody.MovePosition( rigidbody.position + facing * moveSpeed * Time.deltaTime );
					rigidbody.angularVelocity = 0;
				}
				else
				{
					transform.position += (Vector3)(facing * moveSpeed * Time.deltaTime);
				}
				Vector3 dist = targetLocation-transform.position;
				dist.z = 0;
				if(dist.sqrMagnitude<distanceFromTarget*distanceFromTarget)
				{
					endMovement();
					//getWanderSpot();//callback 
				}
			//}
			//else
			//{
				/*if(chase && Vector3.Distance(target.position, myTransform.position) < chaseDistance)
					//enterChase();//callback 
				else
					//enterWander();//callback */
			//}
		}
	}
	public void endMovement()
	{
		currentState = MOVEMENT_STATE.IDLE;
		if( !isServer && !isLocalPlayer )
				return;
		if(moveCallback != null)
		{
			moveCallback();
		}
	}

	void OnDrawGizmos() {
		
		if(currentState==MOVEMENT_STATE.TARGET)
		{
			if(target!=null)
			{
				Gizmos.color = Color.cyan;
				Gizmos.DrawLine(transform.position, target.position);
			}
		}
		else if(currentState==MOVEMENT_STATE.LOCATION)
		{
			if(targetLocation!=null)
			{
				Gizmos.color = Color.cyan;
				Gizmos.DrawLine(transform.position, targetLocation);
			}
		}
		//
	}
}

/*

using UnityEngine;
using System.Collections;

public class BasicEnemy : HurtableCharacter {
	public enum STATES
	{
		WANDER,
		CHASE,
		HURT,
		SIT,
		WAKING
	}
	public STATES currentState = STATES.WANDER;
	//Animator animator;
	protected Transform target;
	public Transform myTransform; //current transform data of this enemy
	public GameObject gem;
	public bool chase = true;
	public bool damageOnTouch = true;
	public float wakingTime = 0;

	public float chaseTime = 5;
	public float chaseDistance = 6.0f;
	public float chaseSpeed = 0.1f; //move speed
	public float wanderTime = 10;
	public float wanderDistance = 1;
	public float wanderSpeed = 0.1f; //move speed
	public bool immune = false;
	protected float rotationSpeed = 3f; //speed of turning
	protected float stateTimer = 5;
	protected Vector3 wanderPosition = new Vector3();
	protected Vector3 facing;
	//
	void OnDrawGizmosSelected() 
	{
		if (target != null && currentState==STATES.CHASE) 
		{
			Gizmos.color = Color.blue;
			Gizmos.DrawLine(transform.position, target.position);
		}
		else if(currentState==STATES.WANDER)
		{
			Gizmos.color = Color.green;
			Gizmos.DrawLine(transform.position, wanderPosition);
		}
	}
	//
	void Awake()
	{
		doAwake();
	}
	void Start()
	{
		doStart();

	}
	void Update()
	{
		doUpdate(); 
	}
	//
	protected void doAwake()
	{
		myTransform = transform; //cache transform data for easy access/preformance
	}
	protected void doStart()
	{
		setupHealth();

		Globals.numEnemies++;
		Globals.enemyList.Add(this);
		//animator = GetComponentsInChildren<Animator>()[0];
		target = GameObject.FindWithTag("Player").transform; //target the player
		if(currentState == STATES.WANDER)
			enterWander();
		if(currentState == STATES.WAKING)
		{
			enterWaiting();
		}
	}
	protected void doUpdate () 
	{
		if(this.isAlive)
		{
			stateTimer -= Time.deltaTime;
			if(currentState==STATES.SIT)
			{
				//DO NOTHING
			}
			if(currentState==STATES.WAKING)
			{
				if (stateTimer<0)
				{
					leaveWaiting();
				}
				else
				{
					doWaiting();
				}
			}
			if(currentState==STATES.CHASE)
			{
				if (stateTimer>0)
				{
					Vector3 dir = myTransform.position - target.position;
					dir.z = 0;
					if(dir.sqrMagnitude>=chaseDistance*chaseDistance)
					{
						enterWander();
						return;
					}
					//myTransform.rotation = Quaternion.LookRotation(Vector3.forward,  dir);
					dir.Normalize();
					//should still get this to work, or remove rigid bodies and do everything with code?
					//rigidbody2D.AddForce(-dir * chaseSpeed * Time.deltaTime);
					myTransform.position += -dir * chaseSpeed * Time.deltaTime;
				}
				else
				{
					enterWander();
				}
			}
			else if(currentState==STATES.WANDER)
			{

				if ( stateTimer>0 )
				{

					myTransform.position += facing * wanderSpeed * Time.deltaTime;
					Vector3 dist = wanderPosition-myTransform.position;
					dist.z = 0;

					if(dist.sqrMagnitude<0.1)
					{
						getWanderSpot();
					}
				}
				else
				{
					if(chase && Vector3.Distance(target.position, myTransform.position) < chaseDistance)
						enterChase();
					else
						enterWander();
				}
			}
			else if(currentState==STATES.HURT)
			{
				if (stateTimer>0)
				{
					transform.Translate(wanderPosition * Time.deltaTime);
				}
				else
				{
					//animator.SetBool("hurt", false);
					enterWander();
				}
			}
		}
	}
	void enterChase()
	{
		currentState = STATES.CHASE;
		stateTimer = this.chaseTime;

		//this is where it might choose a different player
	}

	//
	protected virtual void doWaiting()
	{
	}
	protected virtual void enterWaiting()
	{
		currentState = STATES.WAKING;
		stateTimer = this.wakingTime;
	}
	protected virtual void leaveWaiting()
	{
		currentState = STATES.SIT;
	}

	void enterWander()
	{
		currentState = STATES.WANDER;
		stateTimer = this.wanderTime;
		getWanderSpot();
	}
	void getWanderSpot()
	{
		Vector2 newPosition = Random.insideUnitCircle * wanderDistance;
		wanderPosition.x = myTransform.position.x+newPosition.x;
		wanderPosition.y = myTransform.position.y+newPosition.y;
		wanderPosition.z = myTransform.position.z;
		Vector3 dir = myTransform.position - wanderPosition;
		dir.z = 0;
		//myTransform.rotation = Quaternion.LookRotation(Vector3.forward,  dir);
		facing = myTransform.TransformDirection (-Vector3.up);
	}

	void OnTriggerEnter2D (Collider2D other) 
	{
		if(damageOnTouch && other.name=="Player")
		{
			Player OtherComponent;
			OtherComponent = other.GetComponent ( "Player" ) as Player;
			OtherComponent.hurt(this.transform,10);
			//enterWander();
		}
	}
	public override void die()
	{
		Globals.numEnemies--;
		Globals.enemyList.Remove(this);
		Instantiate(gem, this.myTransform.position+new Vector3(Random.Range(-1,1),Random.Range(-1,1)) , Quaternion.identity);
		base.die();
	}
}

 */