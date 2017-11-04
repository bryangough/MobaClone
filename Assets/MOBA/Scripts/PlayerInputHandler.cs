using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Networking;

public class PlayerInputHandler : NetworkBehaviour {

	MovementHandler movementHandler;
	public GameObject touched;

	// Use this for initialization
	void Start () {
		movementHandler = this.GetComponent<MovementHandler>();
	}
	public override void OnStartLocalPlayer()
	{
		
	}
	// Update is called once per frame
	void Update () {
		if (!isLocalPlayer)
      		return;
		if( Input.GetMouseButtonDown(0) )
		{
			var mousePos = Input.mousePosition;
   			mousePos.z = 10;
			RaycastHit2D hit = Physics2D.Raycast (Camera.main.ScreenToWorldPoint(mousePos), Vector2.zero);
			if(hit.collider != null)
			{
				//square = hit.collider.gameObject.GetComponent<Square>();
				touched = hit.collider.gameObject;

				if(touched.tag == "Ground")
				{
					Vector3 pos = Camera.main.ScreenToWorldPoint(Input.mousePosition);
					pos.z = 0;
					movementHandler.moveToLocation(pos);
				}
				else
				{
					movementHandler.moveToTarget(touched.transform);
				}
			}
		}
	}


}
