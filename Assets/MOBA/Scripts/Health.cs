using UnityEngine;
//using UnityEngine.UI;
using UnityEngine.Networking;
//using System.Collections;

public class Health : NetworkBehaviour { //MonoBehaviour {
  public delegate void HealthChanged(int health);
  public event HealthChanged healthChange;
  public int maxHealth = 1;

  [SyncVar(hook = "OnChangeHealth")]
  public int currentHealth;

//Healthbar should be object pooled
  public GameObject healthBarGameObject;
  public BarControl healthBar;

  public bool destroyOnDeath = true;

  //SyncVars will be set for this
  public override void OnStartClient()
  {

  }
  //SyncVar may not be ready for this
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
          follow.setFollowing(this.gameObject);
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
              RpcShowDeath();
              //Destroy(gameObject);
          }
          else
          {
              currentHealth = 0;
              RpcShowDeath();
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
  void RpcShowDeath()
  {
    if( gameObject != null)
    {
      CombatHandler combatHandler = gameObject.GetComponent<CombatHandler>();
      combatHandler.isActive = false;
    }
    if (  healthBar!= null  )
    {
      ObjectPool.instance.PoolObject(healthBar.gameObject);
    }

    if (isLocalPlayer)
    {
        
        //return to spawn, start repawn timer
        // move back to zero location
        //transform.position = Vector3.zero;
    }
  }
}
