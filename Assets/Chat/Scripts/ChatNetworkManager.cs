using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Networking;
public class ChatNetworkManager : NetworkManager {

	public int numberOfPlayers = 0;
	//public bool sharePlay = false;
	public override void OnClientConnect(NetworkConnection conn) 
	{
		Debug.Log("OnClientConnect");
        ClientScene.AddPlayer(conn, 0);
		//
		/*if( sharePlay )
		{
			ClientScene.AddPlayer(conn, 1);
		}*/
     }
	/*public override void OnServerAddPlayer (NetworkConnection conn, short playerControllerId, NetworkReader extraMessageReader)
	{
		Debug.Log("first.");
	}*/

	public override void OnServerAddPlayer(NetworkConnection conn, short playerControllerId)
    {
		Debug.Log("add player. ");
        GameObject player = (GameObject)Instantiate(playerPrefab, Vector3.zero, Quaternion.identity);
		PlayerChat playerChat = player.GetComponent<PlayerChat>();
		numberOfPlayers++;
		playerChat.userName = "Player"+numberOfPlayers;
		NetworkServer.AddPlayerForConnection(conn, player, playerControllerId);


        /*NetworkPlayerHandler playerComponent = player.GetComponent<NetworkPlayerHandler>();
		//playerComponent.setup();
		playerComponent.gameBoard = gameBoard;
		if(numberOfPlayers==0)
		{
			playerComponent.setup(Team.blue);
		}
		else if(numberOfPlayers==1)
		{
			playerComponent.setup(Team.red);
		}
		else
		{
			Debug.Log("too many players!");
			
			return;
		}
		numberOfPlayers++;
        NetworkServer.AddPlayerForConnection(conn, player, playerControllerId);
		Debug.Log("numberOfPlayers "+numberOfPlayers);
		if(numberOfPlayers==2)
		{
			gameBoard.startGame();
		}*/
    }

	public void OnPlayerDisconnected(NetworkPlayer player)
	{
		numberOfPlayers--;
		Debug.Log("disconnect player.");
	}
}

