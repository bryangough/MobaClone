using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CombatHandler : MonoBehaviour 
{
	public Team team;
	TargetableObject _targetedObject;

	public bool isActive = true;
	// Use this for initialization
	[SerializeField]
	public TargetableObject target
	{
		get { return _targetedObject; }
		set { _targetedObject = value; }
	}
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () 
	{
		if( _targetedObject != null )	
		{
			//attach target with basic shot.
		}
	}
}
