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

    [MenuItem("Assets/Create/Cannon Option")]
    public static void CannonOption()
    {
        CannonOption asset = ScriptableObject.CreateInstance<CannonOption> ();
        AssetDatabase.CreateAsset (asset, "Assets/NewCannonOption.asset");
        AssetDatabase.SaveAssets ();
        EditorUtility.FocusProjectWindow ();
        Selection.activeObject = asset;
    }

    [MenuItem("Assets/Create/Tank Base Option")]
    public static void TankOption()
    {
        TankBaseOption asset = ScriptableObject.CreateInstance<TankBaseOption> ();
        AssetDatabase.CreateAsset (asset, "Assets/NewTankOption.asset");
        AssetDatabase.SaveAssets ();
        EditorUtility.FocusProjectWindow ();
        Selection.activeObject = asset;
    }

    [MenuItem("Assets/Create/Tank Module Option")]
    public static void TankModule()
    {
        ModuleOption asset = ScriptableObject.CreateInstance<ModuleOption> ();
        AssetDatabase.CreateAsset (asset, "Assets/NewTankOption.asset");
        AssetDatabase.SaveAssets ();
        EditorUtility.FocusProjectWindow ();
        Selection.activeObject = asset;
    }
    
}