using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Networking;

public class PlayerChat : NetworkBehaviour {

	public delegate void PlayerChatChange(PlayerChat player, string message, MessageType type);
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
	// Updates my own username.
	//This does not require a RPC because userName is a SyncVar with a hook
	public void UpdateMyName(string newName)
	{
		//check if username is unique

		//
		if(isLocalPlayer)
		{
			CmdUpdateMyName(newName);
		}
	}
	[Command]
	void CmdUpdateMyName(string newName)
  	{
		  userName = newName;
  	}
	// Sends the text message to the server
	public void SendChatMessage(string message)
	{
		CmdSendChatMessage(message, MessageType.Normal);
	}
	[Command]
	void CmdSendChatMessage(string message, MessageType type)
  	{
		  // check for dirty language
		
		  RpcSendChatMessage(message, type);
  	}

//Happens on client

	[ClientRpc]
	void RpcSendChatMessage(string message, MessageType type)
	{
		print(userName+" "+message);
		if( newMessage != null)
		{
			print(newMessage+" newMessage");
			newMessage(this, message, type);
		}
	}
	[TargetRpc]
    public void RpcSendPrivateMessage(NetworkConnection target)
    {
        //MessageType.Private
    }

	//To send messages only to my team
	//Have list of gameObjects in team
	//call this for each client
	//connectionToClient? don't think so.
	[TargetRpc]
    public void RpcSendTeamChatMessage(NetworkConnection target, string message, MessageType type)
    {
        //MessageType.Private
    }
	void OnUserNameChange(string newName)
	{
		if( nameChanged != null)
		{
			nameChanged(newName);
		}
	}
}
