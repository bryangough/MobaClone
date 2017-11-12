using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Networking;

public class MobaNetworkManager : NetworkManager {

//before this player should already be on teams and have their characters selected - from the lobby

	public override void OnServerAddPlayer(NetworkConnection conn, short playerControllerId, NetworkReader extraMessageReader)
    {
        Debug.Log("adding player.");
        //GameObject player = (GameObject)Instantiate(playerPrefab, Vector3.zero, Quaternion.identity);
        //NetworkServer.AddPlayerForConnection(conn, player, playerControllerId);
        //NetworkServer.ReplacePlayerForConnection
        //Debug.Log("create player");
        OnServerAddPlayer(conn, playerControllerId, extraMessageReader);
    }

}
