using UnityEngine;
using System.Collections;
using UnityEngine.Networking;
public class Bullet : NetworkBehaviour 
{
  BasicPower payload;
  TargetableObject target;
  GameObject owner;
  
  public void initialize(BasicPower payload, TargetableObject target, GameObject owner)
  {
    this.payload = payload;
    this.target = target;
    this.owner = owner;
  }
  public void deliverPayload()
  { 
    if( isServer )
    {
      target.health.takeDamage(payload.dmg);
    }
    Destroy(gameObject);
  }
 /* void OnCollisionEnter(Collision collision)
  {
    var hit = collision.gameObject;
    var health = hit.GetComponent<Health>();
    if (health != null)
    {
      health.takeDamage(10);
    }

    Destroy(gameObject);
  }*/
}
