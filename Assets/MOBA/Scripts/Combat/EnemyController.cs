using UnityEngine;
using UnityEngine.Networking;
//using System.Collections;

public class EnemyController : NetworkBehaviour
{

  public GameObject bulletPrefab;
  public Transform bulletSpawn;
  public float distance = 1000;

  public GameObject[] listOfPlayers;

  [SyncVar(hook = "OnChangePlayerToAttack")]
  public GameObject playerToAttack;

  float coolOffTime = 0.0f;

  void Update()
  {
    // only execute the following code if local player ...
    if (!isServer)
      return;

    listOfPlayers = GameObject.FindGameObjectsWithTag("Player");
    if (listOfPlayers.Length > 0)
    {

      float distance = 100f;
      foreach (var player in listOfPlayers)
      {
        float d = Vector3.Distance(transform.position, player.transform.position);
        if (d < distance)
        {
          distance = d;
          this.playerToAttack = player;
        }
      }


      if (this.playerToAttack != null)
      {
        Vector3 direction = playerToAttack.transform.position - transform.position;

        this.transform.rotation =
          Quaternion.Slerp(this.transform.rotation,
          Quaternion.LookRotation(direction), 0.1f);

        float d = Vector3.Distance(transform.position, playerToAttack.transform.position);
        if (d < 15.0f)
        {
          if(this.coolOffTime<Time.time)
          {
            CmdFire();
            this.coolOffTime = Time.time + 1.0f;
          }
        }
      }
    }
  }

  void OnChangePlayerToAttack(GameObject player)
  {
    this.playerToAttack = player;
  }

  [Command]
  void CmdFire()
  {
    // Create the Bullet from the Bullet Prefab
    var bullet = (GameObject)Instantiate(
        bulletPrefab,
        bulletSpawn.position,
        bulletSpawn.rotation);

    // Add velocity to the bullet
    bullet.GetComponent<Rigidbody>().velocity = bullet.transform.forward * 6;

    // Spawn the bullet on the Clients
    NetworkServer.Spawn(bullet);

    // Destroy the bullet after 2 seconds
    Destroy(bullet, 2.0f);
  }

}
