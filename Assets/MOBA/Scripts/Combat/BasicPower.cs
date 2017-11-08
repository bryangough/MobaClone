using UnityEngine;
using System.Collections;

[System.Serializable]
[AddComponentMenu("Combat/Basic Power")]
public class BasicPower : ScriptableObject
{
	public string weaponname = "gun";
	public string animationType = "gun";
	public Sprite buttonImg;
	public int AIPower  = 1;
	public int dmg  = 2;
	public float acc = 0.9f;
	public int range = 5;//find out what melee is
	//attack, buff, heal
	public string type = "";
	public int cost = 4;
	public GameObject bullet;

	//attack visual?
	//attack shot image

	//point, area, cone, line
	//type, area, pierces
	public string attackType = "";

	public int cooldown = 0;

	public bool onCooldown = false;
	public string description = "";

	//bonuses / addition / upgrades
}
