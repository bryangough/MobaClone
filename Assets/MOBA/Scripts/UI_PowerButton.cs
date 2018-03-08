using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class UI_PowerButton : MonoBehaviour {


	public UserPower boundPower;
	public Text countDownText;
	public GameObject icon;
	public Text keyText;
	// Use this for initialization
	void Start () {
		countDownText.text = "";
		setEnabled(false);
	}
	public void setBound(UserPower power)
	{
		boundPower = power;
		setEnabled(true);
	}
	public void setEnabled(bool value)
	{
		if(icon!=null)
			icon.SetActive(value);
		keyText.enabled = value;
		countDownText.enabled = value;
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
	public void usePower()
	{
		if(boundPower.isInitialized)
		{
			boundPower.usePower();
		}
		//combatHandler.usePower(1);
	}
}
