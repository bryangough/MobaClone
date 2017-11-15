using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Networking;
public class AutoHostNetwork : MonoBehaviour {

	private NetworkManager manager;
	public bool autoStartHost = true;
	void Awake()
	{
		manager = GetComponent<NetworkManager>();
	}
 
	void Start () 
	{
		#if UNITY_EDITOR
		if(autoStartHost)
		{
			manager.StartHost();
		}
		#endif
	}
}
