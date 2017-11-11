using UnityEngine;
using System.Collections;

[ExecuteInEditMode]
[System.Serializable]
public class PathPoint : MonoBehaviour 
{
	//public string name = "Path";
	public PathPoint next;
	public float delay = 0f;
	//
	float offsetx = 0;
	float offsety = 0;
	void Start()
	{
		
	}
	
}


