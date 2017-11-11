using UnityEngine;
using UnityEngine.Networking;
public class GruntSpawner : NetworkBehaviour {
	public Team team;
	public GameObject gruntPrefab;
	public PathObject path;

	public override void OnStartServer()
	{

	}

	public void spawnWave()
	{
		for (int i = 0; i < 3; i++)
		{
			var spawnPosition = new Vector3(
				Random.Range(-8.0f, 8.0f),
				0.0f,
				Random.Range(-8.0f, 8.0f));

			GameObject grunt = (GameObject)Instantiate(gruntPrefab, spawnPosition, Quaternion.identity);
//			CombatHandler gruntObj = grunt.GetComponent<CombatHandler>();
			//
			NetworkServer.Spawn(grunt);
		}
	}
}
