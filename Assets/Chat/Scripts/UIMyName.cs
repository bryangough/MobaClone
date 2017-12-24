using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class UIMyName : MonoBehaviour {
	public InputField userNameField;

	public MyChatPlayer myPlayer;
	public PlayerChat playerChat;
	void Start () {
		myPlayer.playerSet += playerIsSet;
		userNameField.interactable = false;
	}
	void playerIsSet()
	{
		playerChat = myPlayer.myPlayer.GetComponent<PlayerChat>();
		userNameField.interactable = true;
		userNameField.text = playerChat.userName;
	}

	public void OnEndEditInput()
    {
		if(userNameField.text.Length>0)
		{
			playerChat.UpdateMyName(userNameField.text);
		}
		else
		{
			userNameField.text = playerChat.userName;
		}
    }
	//UpdateMyName

	
}
