using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
public class TankOptionControl : MonoBehaviour {

	public BaseOption option;
	public Text label;
	public Image displayImage;
	public Toggle toggle;

	void Start()
	{
		toggle = this.GetComponent<Toggle>();
		ToggleGroup toggleGroup = this.GetComponentInParent<ToggleGroup>();
		if( toggleGroup != null )
		{
			toggle.group = toggleGroup;
			toggle.isOn = false;
		}
		if( option!= null)
		{
			label.text = option.optionName;
			displayImage.sprite = option.ui_image;
		}
	}
	
}
