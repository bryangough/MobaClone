using UnityEngine;
using UnityEditor;
using System.Collections;
using System.Collections.Generic;
using System.IO;

[CanEditMultipleObjects]
[CustomEditor(typeof(PathObject))]

public class PathObjectEditor : Editor {
	//
	public static bool isLoop = false;


	//
	PathObject path;
	void OnEnable ()
	{
		path = target as PathObject;
		//touchAction = touchObjectControl.touchAction;
		// we want to start toolbar where we left off
		//toolBar = EditorPrefs.GetInt("index",0);
		if(path==null)
			return;
		
		Tools.hidden = true;
		path.gameObject.SetActive(true);
	}
	void OnDisable()
	{
		if(path==null)
			return;
		Tools.hidden = false;
		if(path.childIsActive(Selection.activeTransform))
		{
		}
		else
		{
			path.gameObject.SetActive(false);
		}
	}

	public override void OnInspectorGUI ()
	{
		
		var pasfd = EditorGUILayout.ObjectField("PathPoint prefab", path.pathPrefab, typeof(PathPoint));
		path.pathPrefab = pasfd as GameObject;

		GUI.color = Color.green;
		if(GUILayout.Button("Add Point",EditorStyles.miniButton))
		{
			GameObject pathPoint = (GameObject)Instantiate(path.pathPrefab, new Vector3(0,0,0), Quaternion.identity);
			pathPoint.transform.parent = path.transform;
			PathPoint p = pathPoint.GetComponent<PathPoint>();
			path.pathPoints.Add(p);
		}

		GUI.color = Color.white;
		if(GUILayout.Button("Refresh Points",EditorStyles.miniButton))
		{
			path.refreshPoints();
		}
		DrawDefaultInspector();
	}
	public void OnSceneGUI () {
		if(path.pathPoints==null)
			return;
		/*Vector3 last = Vector3.zero;
		for(int i = 0; i < path.pathPoints.Length; i++)
		{
			if(i==0)
				Handles.color = Color.red;
			else
				Handles.color = Color.white;
			path.pathPoints[i].position = Handles.FreeMoveHandle( path.pathPoints[i].position, Quaternion.identity, 0.25f, Vector3.zero, drawFunc);  

			if(i > 0)
				Handles.DrawLine(last, path.pathPoints[i].position);
			
			last = path.pathPoints[i].position;
		}*/
	}

}
