using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Networking;

public class PlayerUserInfo : NetworkBehaviour {

//	string userName = "Bob";
	//should gold and experience be synced to all users?
	public int gold = 0;
	public int experience = 0;
	[SyncVar]
	public int level = 1;

	//public Items[] itemList;
	//called by server
	public void gainExperience(int xp)
	{
		bool gainedLevel = false;
		//if xp raised level
		TargetGainExperience(connectionToClient, xp, gainedLevel);
	}

	[TargetRpc]
    public void TargetGainExperience(NetworkConnection target, int gainedExperience, bool gainedLevel)
    {
       experience += gainedExperience;
    }
	[TargetRpc]
    public void TargetGainGold(NetworkConnection target, int gainedGold)
    {
       gold += gainedGold;
    }

	[TargetRpc]
    public void TargetGainGoldAndXP(NetworkConnection target, int gainedGold, int gainedExperience, bool gainedLevel)
    {
		experience += gainedExperience;
       	gold += gainedGold;
    }
}
