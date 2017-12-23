using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FollowObject : MonoBehaviour, IPoolable {


 	GameObject following;
	public float offset = 1f;
	// Use this for initialization
	void Start () {
		//gameObject.SetActive(false);
	}
	public void reset()
	{
		following = null;
		gameObject.SetActive(false);
	}
	public void setFollowing(GameObject followMe)
	{
		following = followMe;
		gameObject.SetActive(true);
		//

		Vector3 pos = following.transform.position;
		pos.y += offset;
		gameObject.transform.position =  pos;
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
