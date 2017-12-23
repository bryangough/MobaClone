using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Networking;

public class PlayerChat : NetworkBehaviour {

	public delegate void PlayerChatChange(PlayerChat player, string message);
  	public event PlayerChatChange newMessage;

	public delegate void PlayerNameChanged(string name);
	public event PlayerNameChanged nameChanged;

	[SyncVar(hook = "OnUserNameChange")]
	public string userName = "User";

	public override void OnStartClient()
	{
		base.OnStartClient();
		LocalPlayerList localPlayerList = FindObjectOfType(typeof(LocalPlayerList)) as LocalPlayerList;
		localPlayerList.AddPlayer(this);
		print("Player started.");
	}
	public void OnDestroy()
	{
		LocalPlayerList localPlayerList = FindObjectOfType(typeof(LocalPlayerList)) as LocalPlayerList;
		if(localPlayerList!=null)
		{
			localPlayerList.RemovePlayer(this);
		}
		
	}
	public override void OnStartLocalPlayer()
	{
		base.OnStartLocalPlayer();

		MyChatPlayer myPlayer = FindObjectOfType(typeof(MyChatPlayer)) as MyChatPlayer;
		myPlayer.myPlayer = this.gameObject;

		//register this UI
	}
	


	public void SendChatMessage(string message)
	{
		CmdSendChatMessage(message);
	}
	[Command]
	void CmdSendChatMessage(string message)
  	{
		  // check for dirty language
		
		  RpcSendChatChat(message);
  	}

//Happens on client

	[ClientRpc]
	void RpcSendChatChat(string message)
	{
		print(userName+" "+message);
		if( newMessage != null)
		{
			print(newMessage+" newMessage");
			newMessage(this, message);
		}
	}
	void OnUserNameChange(string newName)
	{
		if( nameChanged != null)
		{
			nameChanged(newName);
		}
	}
}
