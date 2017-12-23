using System.Collections;
using System.Collections.Generic;
using UnityEngine;

//this will highlight the selected character
public class SelectedHandler : MonoBehaviour 
{

	FollowObject followObject;
	public MyPlayer myPlayer;
	CombatHandler combatHandler;
	SpriteRenderer spriteRenderer;
	// Use this for initialization
	void Start () {
		followObject = this.GetComponent<FollowObject>();
		spriteRenderer = this.GetComponent<SpriteRenderer>();
		myPlayer.playerSet += playerIsSet;
		spriteRenderer.enabled = false;
	}
	public void playerIsSet()
	{
		combatHandler = myPlayer.myPlayer.GetComponent<CombatHandler>();
		combatHandler.targetChanged += moveSelected;
	}
	public void moveSelected()
	{
		if( combatHandler.target != null )
		{
			followObject.setFollowing(combatHandler.target.gameObject);
			spriteRenderer.enabled = true;
		}
		else
		{
			spriteRenderer.enabled = false;
			followObject.reset();
		}
		
	}
	
}
