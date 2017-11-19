using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class BaseOption : ScriptableObject
{
	public GameObject prefab;
	public Sprite ui_image;
	public string optionName;
	public string description;

}
