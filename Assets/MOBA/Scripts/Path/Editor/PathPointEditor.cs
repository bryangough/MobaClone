using UnityEngine;
using UnityEditor;
using System.Collections;
using System.Collections.Generic;
using System.Reflection;
using System.IO;

[CanEditMultipleObjects]
[CustomEditor(typeof(PathPoint))]
public class PathPointEditor : Editor
{
	PathPoint me;
	PathObject myLeader;
	//

	//
	void OnEnable ()
	{
		me = target as PathPoint;
		//Tools.hidden = true;
		if(me == null || me.transform.parent==null)
			return;
		
		myLeader = me.transform.parent.GetComponent<PathObject>();
		myLeader.gameObject.SetActive(true);
	}
	void OnDisable()
	{
		//Tools.hidden = false;
		if(myLeader==null)
			return;
		if(myLeader.childIsActive(Selection.activeTransform))
		{
		}
		else
		{
			myLeader.gameObject.SetActive(false);
		}
	}
	public override void OnInspectorGUI ()
	{
		//DrawDefaultInspector();
		//var pasfd = EditorGUILayout.ObjectField("PathPoint prefab", path.pathPrefab, typeof(PathPoint));
		//path.pathPrefab = pasfd as GameObject;

		//GUI.color = Color.green;
		DrawDefaultInspector();
		if(GUILayout.Button("Select Next",EditorStyles.miniButton))
		{
			
		}
	}
	//public override void OnInspectorGUI() {
		/*serializedObject.Update();
		var controller = target as PathPoint;
		EditorGUIUtility.LookLikeInspector();
		SerializedProperty tps = serializedObject.FindProperty ("next");
		EditorGUI.BeginChangeCheck();
		EditorGUILayout.PropertyField(tps, true);
		if(EditorGUI.EndChangeCheck())
			serializedObject.ApplyModifiedProperties();
		EditorGUIUtility.LookLikeControls();*/
	//}
}
