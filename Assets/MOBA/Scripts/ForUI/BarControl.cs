using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BarControl : MonoBehaviour {

	public RectTransform bar;
	public float barPercent;
	public float width;
	// Use this for initialization
	void Start () {
		width = bar.sizeDelta.x;
		bar.sizeDelta = new Vector2(barPercent * width, bar.sizeDelta.y);
	}
	
	public void updateBar()
	{
		bar.sizeDelta = new Vector2(barPercent * width, bar.sizeDelta.y);
	}
}
