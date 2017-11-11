using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class PathObject : MonoBehaviour {

	//public PathPoint[] pathPoints;
	//[SerializeField]
	public List<PathPoint> pathPoints = new List<PathPoint>();
	public GameObject pathPrefab;
	//
	//public string pathID = "test1";
	//
	void OnDrawGizmos() {
		Vector3 last = Vector3.zero;
		for(int i = 0; i < pathPoints.Count; i++)
		{
			
			if(pathPoints==null)
				return;
			Gizmos.color = Color.blue;
			if(pathPoints[i]!=null)
			{
				if(i > 0)
					Gizmos.DrawLine(last, pathPoints[i].transform.position);
				last = pathPoints[i].transform.position;
			}
		}
	}
	public bool childIsActive(Transform selected)
	{
		foreach(Transform child in transform)
		{
			if(selected == child.transform)
			{
				return true;
			}
		}
		if(transform==selected)
			return true;
		return false;
	}
	public void refreshPoints()
	{
		pathPoints.RemoveAll(PathPoint => PathPoint == null);
		for(int i = 0; i < pathPoints.Count; i++)
		{
			if(pathPoints[i])
			{
				pathPoints[i].name = this.name +"_"+ i;
				if(i!=0)
					pathPoints[i-1].next = pathPoints[i];
			}	
		}
		pathPoints.RemoveAll(PathPoint => PathPoint == null);
	}
	//
}
