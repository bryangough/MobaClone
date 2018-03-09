using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class UIPlayerNameHandler : MonoBehaviour {

	public Text playerNameText;
	public PlayerChat playerChat;


	public void setInit(PlayerChat playerChat)
	{
		this.playerChat = playerChat;
		playerChat.nameChanged += updateName;
		//time?
		updateName(playerChat.userName);
	}
	public void updateName(string newName)
	{
		playerNameText.text = ""+newName+"";
	}
		
		
}
