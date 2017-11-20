using UnityEngine;
using UnityEngine.Networking;
public class GruntSpawner : NetworkBehaviour {
	public Team team;
	public PathObject path;
	
	float randomRange = 0.5f;
	public override void OnStartServer()
	{

	}

	public void spawnWave(SpawnDataOrder data)
	{
		if(!this.isActiveAndEnabled)
		{
			return;
		}
		for (int i = 0; i < data.number; i++)
		{
			var spawnPosition = new Vector3(
				Random.Range(-randomRange, randomRange),
				Random.Range(-randomRange, randomRange),
				0.0f);

			GameObject grunt = (GameObject)Instantiate(data.unitPrefab, this.transform.position + spawnPosition, Quaternion.identity);
			CombatHandler gruntObj = grunt.GetComponent<CombatHandler>();
			gruntObj.team = this.team;
			grunt.name = "Grunt "+this.team;

			

			WaypointMover gruntWaypoint = grunt.GetComponent<WaypointMover>();
			gruntWaypoint.path = path;
			gruntWaypoint.offset = spawnPosition;
			//
			NetworkServer.Spawn(grunt);
		}
	}
}
