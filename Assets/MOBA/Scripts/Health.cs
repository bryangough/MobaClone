using UnityEngine;
//using UnityEngine.UI;
using UnityEngine.Networking;
//using System.Collections;

public class Health : NetworkBehaviour { //MonoBehaviour {
  public delegate void HealthChanged();
  public event HealthChanged healthChange;
  public int maxHealth = 100;

  [SyncVar(hook = "OnChangeHealth")]
  public int currentHealth;

//Healthbar should be object pooled
  public GameObject healthBarGameObject;
  public RectTransform healthBar;

  public bool destroyOnDeath;

  public override void OnStartClient()
  {
    currentHealth = maxHealth;
    if(healthBar!=null)
      healthBar.sizeDelta = new Vector2(currentHealth, healthBar.sizeDelta.y);
  }

  public void takeDamage(int amount)
  {
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
    if(healthBar!=null)
      healthBar.sizeDelta = new Vector2(currentHealth, healthBar.sizeDelta.y);
  }

  void OnChangeHealth(int health)
  {
    Debug.Log(health);
      if(healthBar!=null)
        healthBar.sizeDelta = new Vector2(health, healthBar.sizeDelta.y);
      if( healthChange!= null )
      {
        healthChange();
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
