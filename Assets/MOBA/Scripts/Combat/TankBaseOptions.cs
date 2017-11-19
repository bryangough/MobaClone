using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
[AddComponentMenu("Combat/Tank Base")]
//to keep cleaner. These are kept as ints.
public class TankBaseOptions : BaseOption
{
	public int armor;
	public int speed;
}

