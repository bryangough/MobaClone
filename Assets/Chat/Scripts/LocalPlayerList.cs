using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LocalPlayerList : MonoBehaviour {
	
  	public event PlayerListChange addPlayer;
	public event PlayerListChange removePlayer;
	protected List<PlayerChat> _players = new List<PlayerChat>();
	
	public List<PlayerChat> players
	{
		get { 
			return _players; 
		}
		/*set { 
			
		}*/
	}

	// Use this for initialization
	//Some of this was taken from the lobby example
	public void AddPlayer(PlayerChat player)
	{
		if (_players.Contains(player))
			return;

		_players.Add(player);
		
		if(addPlayer!=null)
			addPlayer(player);
	//	PlayerListModified();
	}

	public void RemovePlayer(PlayerChat player)
	{
		_players.Remove(player);
		
		if(removePlayer!=null)
			removePlayer(player);

		//PlayerListModified();
	}

	public void PlayerListModified()
	{
		/*int i = 0;
		foreach (PlayerChat p in _players)
		{
			p.OnPlayerListChanged(i);
			++i;
		}*/
	}
}
