using System.Collections;
using System.Collections.Generic;
using UnityEngine;


//this should only be active either on the server or on the player
public class PowerHandler : MonoBehaviour {

	public BasicPower[] powerData;
	public UserPower[] powers;
	public float cooldown;
	//public 
	// Use this for initialization
	void Start () {
		CombatHandler combatHandler = this.GetComponent<CombatHandler>();
		powers = new UserPower[powerData.Length];
		for( int x=0;x<powerData.Length;x++)
		{
			UserPower userPower = new UserPower();
			userPower.initialize(combatHandler, powerData[x]);
			powers[x] = userPower;
		}
	}
	
	// Update is called once per frame
	void Update () {
		//update
		for( int x=0;x<powers.Length;x++)
		{
			powers[x].updateCooldown(Time.deltaTime);
		}
		if(powers.Length>0)
		{
			cooldown = powers[0].coolDownCounter;
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

	public BasicPower getPower(int id)
	{
		if( id >= powers.Length)
		{
			return null;
		}
		return powers[id].power;
	}
}
