using System.Collections;
using System.Collections.Generic;
using UnityEngine;


//this allows objects to target them?
public class TargetableObject : MonoBehaviour 
{
    public Health health;
    public CombatHandler combatHandler;
    void Start () 
    {
		health = this.gameObject.GetComponent<Health>();
        combatHandler = this.gameObject.GetComponent<CombatHandler>();
	}

    public bool isAlive()
    {
        return combatHandler.isActive;
    }
}
