using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
public class UI_TopBar : MonoBehaviour {


	public GameTimer timer;

	public Text time;
	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
		//add a delay to this!
		time.text = timer.getTimeString();
	}
}
