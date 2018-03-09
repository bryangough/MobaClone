using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class UIPlayerInput : MonoBehaviour {

	public MyChatPlayer myPlayer;
	public PlayerChat playerChat;
	public InputField inputField;
	public Button sumbmitButton;

	void Start () {
		myPlayer.playerSet += playerIsSet;
		sumbmitButton.interactable = false;
		inputField.interactable = false;
	}
	void playerIsSet()
	{
		playerChat = myPlayer.myPlayer.GetComponent<PlayerChat>();
		
		
		inputField.interactable = true;
	}

	public void OnChangeInputField()
	{
		sumbmitButton.interactable = true;
 		//Debug.Log("Text has been changed.");
	}
	public void OnEndEditInput()
    {
		sumbmitButton.interactable = true;
        /*if (inputField.text.Length > 0)
        {
            Debug.Log("Text has been entered");
        }
        else if (inputField.text.Length == 0)
        {
            Debug.Log("Main Input Empty");
        }*/
    }
	public void SubmitText()
	{
		string sendString = inputField.text;
		if(sendString!="")
		{
			playerChat.SendChatMessage(sendString);
			sumbmitButton.interactable = true;
			inputField.text = "";
		}
	}
}
