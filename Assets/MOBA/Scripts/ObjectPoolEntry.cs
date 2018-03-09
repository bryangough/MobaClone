//Based on a class 
//From here: https://forum.unity.com/threads/simple-reusable-object-pool-help-limit-your-instantiations.76851/

using System.Collections;
using System.Collections.Generic;
using UnityEngine;


/// <summary>
/// Member class for a prefab entered into the object pool
/// </summary>
[System.Serializable]
public class ObjectPoolEntry
{
		/// <summary>
		/// the object to pre instantiate
		/// </summary>
		[SerializeField]
		public GameObject Prefab;
		/// <summary>
		/// quantity of object to pre-instantiate
		/// </summary>
		[SerializeField]
		public int Count;
		[HideInInspector]
		public Stack<GameObject> pool;
		[HideInInspector]
		public int objectsInPool = 0;
		public void init()
		{
			pool = new Stack<GameObject>();
		}
		public GameObject getItem()
		{
			if( objectsInPool > 0)
			{
				objectsInPool--;
				return pool.Pop();
			}
			return null;
		}
		public void addItem(GameObject obj)
		{
			objectsInPool++;
			//check if correct type?
			pool.Push(obj);
		}
}