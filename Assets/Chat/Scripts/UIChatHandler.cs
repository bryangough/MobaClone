using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class UIChatHandler : MonoBehaviour {

	public LocalPlayerList myList;
	public int maxNumberOfText = 20;
	public GameObject textPrefab;
	public GameObject messageArea;
	List<GameObject> messages = new List<GameObject>();

	void Start ()
	{
		print("UIChat start "+myList.players.Count);

		myList.addPlayer += addPlayer;
		if(myList.players.Count>0)
		{
			for(int x=0;x<myList.players.Count;x++)
			{
				addPlayer( myList.players[x] );
			}
		}
	}
	public void addPlayer(PlayerChat player)
	{
		print("add player"+player);
		player.newMessage += messageFromPlayer;
	}
	public void messageFromPlayer(PlayerChat player, string message, MessageType type)
	{
		print("messageFromPlayer");
		addText(player, message);
	}
//Chat messages won't be synced by the server. Newer players won't see older messages
	public void addText( PlayerChat player, string message  )
	{
		//create game object
		GameObject gameObject = Instantiate(textPrefab, Vector3.zero , Quaternion.identity);
		Text text = gameObject.GetComponent<Text>();
		//time?
		text.text = ""+player.userName +": "+ message;
		gameObject.transform.parent = messageArea.transform;
		gameObject.transform.SetSiblingIndex(0);
		messages.Add(gameObject);
print("addText");
		if(messages.Count>maxNumberOfText)
		{
			//remove
			print("Remove extra messages");
		}
	}
}
