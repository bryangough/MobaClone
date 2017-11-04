using UnityEngine;
using UnityEngine.Networking;
using System.Collections;

public class TutPlayerController : NetworkBehaviour
{

  public GameObject bulletPrefab;
  public Transform bulletSpawn;

  public override void OnStartLocalPlayer()
  {
//    GetComponent<MeshRenderer>().material.color = Color.blue;
  }

  void Update()
  {
    // only execute the following code if local player ...
    if (!isLocalPlayer)
      return;

    var x = Input.GetAxis("Horizontal") * Time.deltaTime * 150.0f;
    var z = Input.GetAxis("Vertical") * Time.deltaTime * 3.0f;

    transform.Rotate(0, x, 0);
    transform.Translate(0, 0, z);

    if (Input.GetKeyDown(KeyCode.Space))
    {
      CmdFire();
    }
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

    if(isLocalPlayer)
      bullet.GetComponent<MeshRenderer>().material.color = Color.blue;

    // Spawn the bullet on the Clients
    NetworkServer.Spawn(bullet);

    // Destroy the bullet after 2 seconds
    Destroy(bullet, 2.0f);
  }

}
