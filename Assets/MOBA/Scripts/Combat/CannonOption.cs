using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
[AddComponentMenu("Combat/Cannon Option")]
//to keep cleaner. These are kept as ints.
public class CannonOption : BaseOption
{
	//public BasicPower[] powerData;
	public BasicPower autoPower;
	public BasicPower power1;
	public BasicPower power2;

}
