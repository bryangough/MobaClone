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
	public int range = 40;//find out what melee is
	public int clipsize = 6;
	//attack, buff, heal
	public string type = "";
	public int cost = 4;

	//attack visual?
	//attack shot image

	//point, area, cone, line
	//type, area, pierces
	public string attackType = "";

	public int cooldown = 0;
	public string description = "";

	//bonuses / addition / upgrades
}
