using UnityEngine;
//using UnityEngine.UI;
using UnityEngine.Networking;
//using System.Collections;

public class Health : NetworkBehaviour { //MonoBehaviour {
  public const int maxHealth = 100;

  [SyncVar(hook = "OnChangeHealth")]
  public int currentHealth = maxHealth;

  public RectTransform healthBar;

  public bool destroyOnDeath;

  public override void OnStartClient()
  {
    healthBar.sizeDelta = new Vector2(currentHealth, healthBar.sizeDelta.y);
  }

  public void TakeDamage(int amount)
  {
    currentHealth -= amount;
    if (currentHealth <= 0)
    {
      if (destroyOnDeath)
      {
        Destroy(gameObject);
      }
      else
      {
        currentHealth = maxHealth;

        // called on the Server, will be invoked on the Clients
        RpcRespawn();
      }
    }

    //healthBar.sizeDelta = new Vector2(currentHealth, healthBar.sizeDelta.y);
  }

  void OnChangeHealth(int health)
  {
    healthBar.sizeDelta = new Vector2(health, healthBar.sizeDelta.y);
  }

  [ClientRpc]
  void RpcRespawn()
  {
    if (isLocalPlayer)
    {
      // move back to zero location
      transform.position = Vector3.zero;
    }
  }
}
