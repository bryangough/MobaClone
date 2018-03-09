using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
[AddComponentMenu("Tank/Module Option")]
public class ModuleOption : BaseOption
{
	public BasicPower power3;
	public BasicPower power4;

	public TankEffector[] effectors;

}
