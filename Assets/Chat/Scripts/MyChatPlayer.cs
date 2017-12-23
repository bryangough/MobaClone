using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MyChatPlayer : MonoBehaviour {
	
	public delegate void MyPlayerSet();
  	public event MyPlayerSet playerSet;
	private GameObject _myPlayer;
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
