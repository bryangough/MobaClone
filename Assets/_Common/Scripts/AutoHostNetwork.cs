using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Networking;

/*

To speed up testing. This script will automatically start the host but only in the editor.

 */
public class AutoHostNetwork : MonoBehaviour {
	#if UNITY_EDITOR
	private NetworkManager manager;
	public bool autoStartHost = true;
	void Awake()
	{
		manager = GetComponent<NetworkManager>();
	}
 
	void Start () 
	{
		
		if(autoStartHost)
		{
			manager.StartHost();
		}
		
	}
	#endif
}
