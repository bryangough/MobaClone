using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Networking;

public class MobaNetworkManager : NetworkManager {

    public GameObject leftStartPoint;
//before this player should already be on teams and have their characters selected - from the lobby
    /*public override void OnClientConnect(NetworkConnection conn) {
         ClientScene.AddPlayer(conn, 0);
     }*/
 
     public override void OnServerAddPlayer (NetworkConnection conn, short playerControllerId) {
         print ("Adding player. ");
         createPlayer(conn, playerControllerId);
         //OnServerAddPlayer (conn, playerControllerId);
     }
	public override void OnServerAddPlayer(NetworkConnection conn, short playerControllerId, NetworkReader extraMessageReader)
    {
        Debug.Log("Adding player. Message");
        //GameObject player = (GameObject)Instantiate(playerPrefab, Vector3.zero, Quaternion.identity);
        //NetworkServer.AddPlayerForConnection(conn, player, playerControllerId);
        //NetworkServer.ReplacePlayerForConnection
        //Debug.Log("create player");
        OnServerAddPlayer(conn, playerControllerId, extraMessageReader);
    }

    public void createPlayer(NetworkConnection conn, short playerControllerId)
    {
        GameObject player;
        Transform startPos = leftStartPoint.transform;
        
        if (startPos != null)
        {
            player = (GameObject)Instantiate(playerPrefab, startPos.position, startPos.rotation);
        }
        else
        {
            player = (GameObject)Instantiate(playerPrefab, Vector3.zero, Quaternion.identity);
        }

        NetworkServer.AddPlayerForConnection(conn, player, playerControllerId);
    }
}
