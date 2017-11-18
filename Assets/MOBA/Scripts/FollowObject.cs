using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FollowObject : MonoBehaviour, IPoolable {


	public GameObject following;
	public float offset = 1f;
	// Use this for initialization
	void Start () {
		
	}
	public void reset()
	{
		following = null;
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
