using UnityEngine;
using System.Collections;
using UnityEditor;

public class MakePower 
{
    [MenuItem("Assets/Create/Power Object")]
    public static void Create()
    {
        BasicPower asset = ScriptableObject.CreateInstance<BasicPower> ();
        AssetDatabase.CreateAsset (asset, "Assets/NewWeaponObject.asset");
        AssetDatabase.SaveAssets ();
        EditorUtility.FocusProjectWindow ();
        Selection.activeObject = asset;
    }
    
}