using System.Collections;
using System.Collections.Generic;
using UnityEngine;


//this should only be active either on the server or on the player
public class PowerHandler : MonoBehaviour {

	public BasicPower[] powers;

	//public 
	// Use this for initialization
	void Start () {
		CombatHandler combatHandler = this.GetComponent<CombatHandler>();
		for( int x=0;x<powers.Length;x++)
		{
			powers[x].initialize(combatHandler);
		}
	}
	
	// Update is called once per frame
	void Update () {
		//update
		for( int x=0;x<powers.Length;x++)
		{
			powers[x].updateCooldown(Time.deltaTime);
		}
	}
	public bool isPowerReady(int id)
	{
		if( id >= powers.Length)
		{
			return false;
		}
		return powers[id].isReady();
	}

	public bool usePower(int id)
	{
		if( id >= powers.Length)
		{
			return false;
		}
		//if power exists
		//if power off-cooldown
		return powers[id].usePower();
	}
}
