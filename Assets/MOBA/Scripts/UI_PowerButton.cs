using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class UI_PowerButton : MonoBehaviour {


	public UserPower boundPower;
	public Text countDownText;
	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
		if(boundPower!=null)
		{
			if(boundPower.onCooldown)
			{
				countDownText.text = Mathf.Floor(boundPower.coolDownCounter).ToString();
			}
		}
	}
}
