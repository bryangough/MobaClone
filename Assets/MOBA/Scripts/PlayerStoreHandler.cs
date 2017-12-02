using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Networking;

//this class will allow the player to spend money in the store.
[RequireComponent(typeof(PlayerUserInfo))]
public class PlayerStoreHandler : NetworkBehaviour 
{

	PlayerUserInfo playerUser;

	void Start()
	{
		playerUser = this.GetComponent<PlayerUserInfo>();
	}

	[Command]
	void CmdPurchaseItem(int itemId)
	{
		//check if near store
		//check if item exists,
		//playerUser.gold
		//check is user has enough gold
		//reply with TargetItemPurchased(connectionToClient, ...
		
	}

	[TargetRpc]
    public void TargetItemPurchased(NetworkConnection target, int itemId, int goldChangce, bool success, string message)
    {
       
    }
	

}
