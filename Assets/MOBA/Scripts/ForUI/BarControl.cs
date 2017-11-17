using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class BarControl : MonoBehaviour {

	public RectTransform bar;
	public float barPercent  = 1.0f;
	public float width;
	public Text displayText;
	// Use this for initialization
	void Start () {
		width = bar.sizeDelta.x;
		bar.sizeDelta = new Vector2(barPercent * width, bar.sizeDelta.y);
	}
	public void setPercent(float num, float total)
	{
		if(total==0)
		{
			barPercent = 0;
		}
		else
		{
			barPercent = num/total;
		}
		if( displayText !=null )
			displayText.text = num +"/"+total;
		updateBar();
	}
	public void updateBar()
	{
		bar.sizeDelta = new Vector2(barPercent * width, bar.sizeDelta.y);
	}
	public void updateBar(float percent)
	{
		bar.sizeDelta = new Vector2(percent * width, bar.sizeDelta.y);
	}
}
