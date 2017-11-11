using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WaypointMover : MovementHandler {

	public PathObject path;
	public PathPoint nextPoint;
	public Vector3 offset;

	void Start () {
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
			Debug.Log("next");
		}
		else
		{
			Debug.Log("done!");
		}
	}
	
	// Update is called once per frame
	void Update () {
		doUpdate();
	}
}
