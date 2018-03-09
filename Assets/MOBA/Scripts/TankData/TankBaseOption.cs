using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
[AddComponentMenu("Tank/Tank Base")]
//to keep cleaner. These are kept as ints.
public class TankBaseOption : BaseOption
{
	public int baseArmor;
	public int baseSpeed;
	public int baseHP;

	public TankEffector[] effectors;
}

