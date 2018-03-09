using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Networking;

/*

To speed up testing. This script will automatically start the host but only in the editor.

 */
public class AutoHostNetwork : MonoBehaviour {
	
	private NetworkManager manager;
	public bool autoStartHost = true;
	//public bool startLocal = true;
	void Awake()
	{
		#if !UNITY_EDITOR
			autoStartHost = false;
		#endif
		manager = GetComponent<NetworkManager>();
	}
 
	void Start () 
	{
		
		if(autoStartHost)
		{
			manager.StartHost();
		}
		/*if(startLocal)
		{
			
		}*/
		
	}
	
}
