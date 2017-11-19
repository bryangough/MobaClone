using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Networking;

public class PlayerUserInfo : NetworkBehaviour {

//	string userName = "Bob";
	// Use this for initialization
	public int gold = 0;
	public int experience = 0;
	[SyncVar]
	public int level = 1;


	public void gainExperience()
	{

	}
}
