using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Networking;
public class AutoHostNetwork : MonoBehaviour {

	public NetworkManager manager;
	void Awake()
	{
		manager = GetComponent<NetworkManager>();
	}
 
	void Start () {
		manager.StartHost();	
	}
}
