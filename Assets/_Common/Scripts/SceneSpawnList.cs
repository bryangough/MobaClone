using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Networking;

/**

NetworkManager needs you to register spawnable prefabs.
Unfortunately, because NetworkManager is a singleton, you must register your prefabs in the lobby.

I've pulled the Spawnable List code out of network manager. This code will allows us to register the prefabs in the
scene that uses them. It will also remvoe those objects if the scene is destroyed.

*/

[AddComponentMenu("NetworkExtra/SceneSpawnList")]
public class SceneSpawnList : MonoBehaviour {

	[SerializeField] List<GameObject> m_SpawnPrefabs = new List<GameObject>();

	public List<GameObject> spawnPrefabs { get { return m_SpawnPrefabs; }}


	internal void RegisterClientMessages(NetworkClient client)
    {

		foreach (var prefab in m_SpawnPrefabs)
		{
			if (prefab != null)
			{
				ClientScene.RegisterPrefab(prefab);
			}
		}
    }

	void OnDestroy()
	{
		//clean out prefabs
	}
}

