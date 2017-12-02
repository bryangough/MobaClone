using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class UserPower 
{
	public BasicPower power;
	public float coolDownCounter = 0;
	public bool onCooldown = false;
	public CombatHandler combatHandler;
	public void initialize (CombatHandler combatHandler, BasicPower power)
	{
		this.power = power;
		this.combatHandler = combatHandler;
	}
    public bool usePower()
	{
		if( this.isReady() )
		{
			coolDownCounter = power.cooldown;
			onCooldown = true;
			//this.combatHandler.usePower(this);
		}
		return false;
	}
	public bool isReady()
	{
		return !onCooldown;
	}
	//clean this up
	public void updateCooldown(float timeDelta)
	{
		if(onCooldown)
		{
			coolDownCounter -= timeDelta;
			if(coolDownCounter<0)
			{
				onCooldown = false;
			}
		}
	}
}
