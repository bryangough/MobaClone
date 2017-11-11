using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Networking;
public class GameTimer : NetworkBehaviour {

	[SyncVar]
	public float time;

	public string timeString;
	
	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
		if( !isServer ) 
			return;
		time += Time.deltaTime;
		
		timeString = getTimeString();
	}
	public string getTimeString()
	{
		int secs = (int)time % 60;
		int mins = (int)time / 60;
		return string.Format("{0:00}:{1:00}", mins, secs);
	}
}
