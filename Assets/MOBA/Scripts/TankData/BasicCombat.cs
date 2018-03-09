using UnityEngine;
using System.Collections;

[AddComponentMenu("Combat/Basic Combat")]
public class BasicCombat : ScriptableObject  
{
	public int movementspeed = 5;
	public int shieldhp = 10;
	public int selfhp = 2;

	public bool attackable = true; // can player attack it
	public bool hostile = true; // will it fight player - false for area based
	public int actionPoints = 4;

	public int meleeDmg = 1;

	public float accuracy = 0.8f;
	//aggro range

	//public 
	//default actions
	public CombatAction[] actions;//? 

}
