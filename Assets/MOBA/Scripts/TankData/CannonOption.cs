using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
[AddComponentMenu("Tank/Cannon Option")]
public class CannonOption : BaseOption
{
	//public BasicPower[] powerData;
	public BasicPower autoPower;
	public BasicPower power1;
	public BasicPower power2;

	public TankEffector[] effectors;

}
