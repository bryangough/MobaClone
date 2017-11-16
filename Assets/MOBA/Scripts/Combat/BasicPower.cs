using UnityEngine;
using System.Collections;

[System.Serializable]
[AddComponentMenu("Combat/Basic Power")]
//to keep cleaner. These are kept as ints.
public class BasicPower : ScriptableObject
{
	//basic
	public string weaponname = "gun";
	public string description = "";


	//
	public int range = 5;
	public int cost = 4;
	public int scaling = 0;//?	
	public int cooldown = 1;
	public int castTime = 0;
	public bool isChannel = false;

	//
	public bool requiresTarget = false;
	public bool alwaysTargetSelf = false;
	public bool isToggle = false;
	public bool isInstant = false;
	//
	public int dmg  = 2;
	public Damage_TYPE damageType = Damage_TYPE.PHYSICAL;



	//ui 
	public Sprite buttonImg;




	//?
	
	public GameObject bullet;



	//Game variables
	
}
