using System.Collections;
using System.Collections.Generic;
using UnityEngine;

//Effectors adjust attributes
// Cannon, Module and Tank Base may change stats
// movement speed, attack power, armor, hp
[System.Serializable]
public class TankEffector
{
	public ATTRIBUTE_TYPE attribute;
	public int modifier;
}
