using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Networking;

public class MobaNetworkManager : NetworkManager {

    public GameObject leftStartPoint;
    public GameObject rightStartPoint;
    public int numberOfPlayers = 0;
    //before this player should already be on teams and have their characters selected - from the lobby
    public override void OnClientConnect(NetworkConnection conn) {
        print("OnClientConnect");
         //ClientScene.AddPlayer(conn, 0);
         createPlayer(conn, 0);
     }

     // Set this in the inspector
    public UnetGameRoom GameRoom;

    void Awake()
    {
        if (GameRoom != null)
        {
            GameRoom.PlayerJoined += OnPlayerJoined;
            GameRoom.PlayerLeft += OnPlayerLeft;
            //Debug.LogError("Game Room property is not set on NetworkManager");
            return;
        }
        else
        {
            print("Awake RegisterHandler");
             NetworkServer.RegisterHandler(MsgType.AddPlayer, OnAddPlayerMessage);
           // createPlayer(player.Connection, 0);
        }

        // Subscribe to events
        
    }

    private void OnPlayerJoined(UnetMsfPlayer player)
    {
        // Spawn the player object (https://docs.unity3d.com/Manual/UNetPlayers.html)
        // This is just a dummy example, you'll need to create your own object (or not)
       // var playerGameObject = new GameObject();
       // NetworkServer.AddPlayerForConnection(player.Connection, playerGameObject, 0);
       createPlayer(player.Connection, 0);
    }

    private void OnPlayerLeft(UnetMsfPlayer player)
    {
        numberOfPlayers--;
    }
 
     /*public override void OnServerAddPlayer (NetworkConnection conn, short playerControllerId) {
         print ("Adding player. ");
         createPlayer(conn, playerControllerId);
         //OnServerAddPlayer (conn, playerControllerId);
     }*/
	/*public override void OnServerAddPlayer(NetworkConnection conn, short playerControllerId, NetworkReader extraMessageReader)
    {
        Debug.Log("Adding player. Message");
        //GameObject player = (GameObject)Instantiate(playerPrefab, Vector3.zero, Quaternion.identity);
        //NetworkServer.AddPlayerForConnection(conn, player, playerControllerId);
        //NetworkServer.ReplacePlayerForConnection
        //Debug.Log("create player");
        OnServerAddPlayer(conn, playerControllerId, extraMessageReader);
    }*/
    public override void OnServerDisconnect(NetworkConnection conn)
    {
        base.OnServerDisconnect(conn);
        // Don't forget to notify the room that a player disconnected
        GameRoom.ClientDisconnected(conn);
    }    
    void OnAddPlayerMessage(NetworkMessage netMsg)
    {
        print("OnAddPlayerMessage");
        createPlayer(netMsg.conn, 0);
    }
    public void createPlayer(NetworkConnection conn, short playerControllerId)
    {
        GameObject player;
        CombatHandler combatHandler;
        /*if( numberOfPlayers%2==1 )
        {
            player = (GameObject)Instantiate(playerPrefab, leftStartPoint.transform.position, Quaternion.identity);
            combatHandler = player.GetComponent<CombatHandler>();
            combatHandler.team = Team.Left;
        }
        else
        {*/
            player = (GameObject)Instantiate(playerPrefab, rightStartPoint.transform.position, Quaternion.identity);
            combatHandler = player.GetComponent<CombatHandler>();
            combatHandler.team = Team.Right;
            Debug.Log("create player");
        //}
        //Debug.Log("create player");
        numberOfPlayers++;
        NetworkServer.AddPlayerForConnection(conn, player, playerControllerId);
    }
}
