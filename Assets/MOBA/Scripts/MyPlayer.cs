using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MyPlayer : MonoBehaviour {

	public delegate void MyPlayerSet();
  	public event MyPlayerSet playerSet;
	private GameObject _myPlayer;
	public PlayerInputHandler inputHandler;

	public GameObject myPlayer
	{
		get { 
			return _myPlayer; 
		}
		set { 
			_myPlayer = value;
			if( playerSet != null)
			{
				playerSet();
			}
		}
	}
}
