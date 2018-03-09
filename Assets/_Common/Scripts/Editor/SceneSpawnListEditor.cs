using System;
using System.Reflection;
using UnityEditorInternal;
using UnityEngine;
using UnityEditor;
using UnityEngine.Networking;
using UnityObject = UnityEngine.Object;

namespace UnityEditor
{
    [CustomEditor(typeof(SceneSpawnList), true)]
    [CanEditMultipleObjects]
    public class SceneSpawnListEditor : Editor
    {
		SerializedProperty m_SpawnListProperty;
		ReorderableList m_SpawnList;


        protected bool m_Initialized = false;
        protected SceneSpawnList m_SceneSpawnList;
		protected void Init()
        {
            if (m_Initialized)
            {
                return;
            }
            m_Initialized = true;
            m_SceneSpawnList = target as SceneSpawnList;	

			m_SpawnListProperty = serializedObject.FindProperty("m_SpawnPrefabs");

            m_SpawnList = new ReorderableList(serializedObject, m_SpawnListProperty);
			
            m_SpawnList.drawHeaderCallback = DrawHeader;
            m_SpawnList.drawElementCallback = DrawChild;
            m_SpawnList.onReorderCallback = Changed;
            m_SpawnList.onAddDropdownCallback = AddButton;
            m_SpawnList.onRemoveCallback = RemoveButton;
            m_SpawnList.onChangedCallback = Changed;
            m_SpawnList.onReorderCallback = Changed;
            m_SpawnList.onAddCallback = Changed;
            m_SpawnList.elementHeight = 16;
		}

		public override void OnInspectorGUI()
        {
            //if (m_DontDestroyOnLoadProperty == null || m_DontDestroyOnLoadLabel == null)
                m_Initialized = false;

            Init();

            serializedObject.Update();
            //EditorGUILayout.PropertyField(m_DontDestroyOnLoadProperty, m_DontDestroyOnLoadLabel);
            //EditorGUILayout.PropertyField(m_RunInBackgroundProperty , m_RunInBackgroundLabel);

            /*if (EditorGUILayout.PropertyField(m_LogLevelProperty))
            {
                LogFilter.currentLogLevel = (int)m_SceneSpawnList.logLevel;
            }*/

            ShowSpawnInfo();
            serializedObject.ApplyModifiedProperties();

            //ShowDerivedProperties(typeof(NetworkManager), null);
        }
		protected void ShowSpawnInfo()
        {
            EditorGUI.BeginChangeCheck();
            m_SpawnList.DoLayoutList();
            if (EditorGUI.EndChangeCheck())
            {
                serializedObject.ApplyModifiedProperties();
            }

            EditorGUI.indentLevel -= 1;
        }


		static void DrawHeader(Rect headerRect)
        {
            GUI.Label(headerRect, "Registered Spawnable Prefabs:");
        }

		internal void DrawChild(Rect r, int index, bool isActive, bool isFocused)
        {
            SerializedProperty prefab = m_SpawnListProperty.GetArrayElementAtIndex(index);
            GameObject go = (GameObject)prefab.objectReferenceValue;

            GUIContent label;
            if (go == null)
            {
                label = new GUIContent("Empty", "Drag a prefab with a NetworkIdentity here");
            }
            else
            {
                var uv = go.GetComponent<NetworkIdentity>();
                if (uv != null)
                {
                    label = new GUIContent(go.name, "AssetId: [" + uv.assetId + "]");
                }
                else
                {
                    label = new GUIContent(go.name, "No Network Identity");
                }
            }

            var newGameObject = (GameObject)EditorGUI.ObjectField(r, label, go, typeof(GameObject), false);
            if (newGameObject == null)
            {
                m_SceneSpawnList.spawnPrefabs[index] = null;
                EditorUtility.SetDirty(target);
                return;
            }
            if (newGameObject.GetComponent<NetworkIdentity>())
            {
                if (m_SceneSpawnList.spawnPrefabs[index] != newGameObject)
                {
                    m_SceneSpawnList.spawnPrefabs[index] = newGameObject;
                    EditorUtility.SetDirty(target);
                }
            }
            else
            {
                if (LogFilter.logError) { Debug.LogError("Prefab " + newGameObject + " cannot be added as spawnable as it doesn't have a NetworkIdentity."); }
            }
        }

		internal void Changed(ReorderableList list)
        {
            EditorUtility.SetDirty(target);
        }

        internal void AddButton(Rect rect, ReorderableList list)
        {
            m_SceneSpawnList.spawnPrefabs.Add(null);
            m_SpawnList.index = m_SpawnList.count - 1;
            EditorUtility.SetDirty(target);
        }

        internal void RemoveButton(ReorderableList list)
        {
            m_SceneSpawnList.spawnPrefabs.RemoveAt(m_SpawnList.index);
            m_SpawnListProperty.DeleteArrayElementAtIndex(m_SpawnList.index);
            if (list.index >= m_SpawnListProperty.arraySize)
            {
                list.index = m_SpawnListProperty.arraySize - 1;
            }
            EditorUtility.SetDirty(target);
        }
	}
}
