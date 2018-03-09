using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Networking;

public class PlayerSyncTransform : NetworkBehaviour {

	public bool ValidateMove(ref Vector2 position, ref Vector2 velocity, ref float rotation)
    {
        Debug.Log("pos:" + position);
		//return false to ignore move
        return true;
    }

	public void whenClientConnects()
    {
       // GetComponent<NetworkTransform>().clientMoveCallback2D = ValidateMove;
    }
}
