using UnityEngine;
//using UnityEngine.UI;
using UnityEngine.Networking;
//using System.Collections;

public class Health : NetworkBehaviour { //MonoBehaviour {
  public delegate void HealthChanged(int health);
  public event HealthChanged healthChange;
  public int maxHealth = 100;

  [SyncVar(hook = "OnChangeHealth")]
  public int currentHealth;

//Healthbar should be object pooled
  public GameObject healthBarGameObject;
  public BarControl healthBar;

  public bool destroyOnDeath;

  public override void OnStartClient()
  {
    currentHealth = maxHealth;
    if(healthBar!=null)
      healthBar.setPercent(currentHealth, maxHealth);
  }

  public void takeDamage(int amount)
  {
    //isServer?
      currentHealth -= amount;
      if (currentHealth <= 0)
      {
          if (destroyOnDeath)
          {
              Destroy(gameObject);
              currentHealth = 0;
          }
          else
          {
              currentHealth = maxHealth;
              // called on the Server, will be invoked on the Clients
              RpcRespawn();
          }
      }
  }

  void OnChangeHealth(int health)
  {
      if(healthBar!=null)
        healthBar.setPercent(health, maxHealth);
      if( healthChange!= null )
      {
        healthChange(health);
      }
  }

  [ClientRpc]
  void RpcRespawn()
  {
    if (isLocalPlayer)
    {
        //return to spawn, start repawn timer
        // move back to zero location
        //transform.position = Vector3.zero;
    }
  }
}
