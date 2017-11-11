using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Networking;

public class WaypointMover : MovementHandler {

	public PathObject path;
	public PathPoint nextPoint;
	public Vector3 offset;

	void Start () {
		if( !isServer )
			return;
		nextPoint = path.getFirstPoint();
		if( nextPoint!=null )
		{
			moveToLocation( nextPoint.transform.position + offset,  atWaypoint);
		}
	}
	 
	void atWaypoint()
	{
		if( nextPoint.next != null)
		{
			nextPoint = nextPoint.next;
			moveToLocation( nextPoint.transform.position + offset,  atWaypoint);
		}
		else
		{
		}
	}

	void continueToWaypoint()
	{
		if( nextPoint != null)
		{
			moveToLocation( nextPoint.transform.position + offset,  atWaypoint);
		}
	}
	
	// Update is called once per frame
	void Update () {
		doUpdate();
	}
}
