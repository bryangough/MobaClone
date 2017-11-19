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

  public bool destroyOnDeath = true;

  void Start()
  {
    currentHealth = maxHealth;
    if( healthBarGameObject != null)
    {
      GameObject bar = ObjectPool.instance.GetObjectForType(healthBarGameObject);
      if( bar != null)
      {
        FollowObject follow = bar.GetComponent<FollowObject>();
        if( follow != null )
        {
          follow.following = this.gameObject;
        }
        healthBar = bar.GetComponent<BarControl>();
      }
      
    }
    if(healthBar!=null)
      healthBar.setPercent(currentHealth, maxHealth);
  }

  public bool takeDamage(int amount, GameObject attacker)
  {
    //isServer?
      currentHealth -= amount;
      if (currentHealth <= 0)
      {
          if (destroyOnDeath)
          {
              currentHealth = 0;
              CombatHandler combatHandler = gameObject.GetComponent<CombatHandler>();
              combatHandler.isActive = false;

              if (  healthBar!= null  )
              {
                ObjectPool.instance.PoolObject(healthBar.gameObject);
              }
              combatHandler.isActive = false;
              
              //Destroy(gameObject);
          }
          else
          {
              CombatHandler combatHandler = gameObject.GetComponent<CombatHandler>();
              combatHandler.isActive = false;
              //currentHealth = maxHealth;
              // called on the Server, will be invoked on the Clients
             // RpcRespawn();
          }
          if( attacker!= null )
          {
//            Debug.Log(attacker.name+" got last hit.");
          }
          return true;
      }
      return false;
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
