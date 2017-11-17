using UnityEngine;
using System.Collections;
using UnityEngine.Networking;
public class Bullet : NetworkBehaviour 
{
  BasicPower payload;
  TargetableObject target;
  public void initialize(BasicPower payload, TargetableObject target)
  {
    this.payload = payload;
    this.target = target;
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
