using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FollowObject : MonoBehaviour {


	public GameObject following;
	public float offset = 1f;
	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () 
	{
		if( following!=null )
		{
			Vector3 pos = following.transform.position;
			pos.y += offset;
			gameObject.transform.position =  pos;
		}
	}
}
