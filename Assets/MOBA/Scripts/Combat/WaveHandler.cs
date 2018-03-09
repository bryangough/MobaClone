using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Networking;
//Server only!
public class WaveHandler : NetworkBehaviour {

	public GruntSpawner[] spawners;
	public GameTimer timer;
	public SpawnData[] spawns;
	public int nextSpawnTime = 0;
	// Use this for initialization
	void Start () {
		//re-order spawns as list by time
		
	}
	void Update () {
		if(!isServer)
		{
			return;
		}
		//timer
		if(nextSpawnTime>=spawns.Length)
			return;
		if(timer.time > spawns[nextSpawnTime].time)
		{
			for(int x=0;x<spawners.Length;x++)
			{
				spawners[x].spawnWave(spawns[nextSpawnTime].spawn);
			}
			nextSpawnTime++;
		}
	}
}
