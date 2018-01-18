using UnityEditor;

/// <summary>
/// Instead of editing this script, I would recommend to write your own
/// (or copy and change it). Otherwise, your changes will be overwriten when you
/// update project :)
/// </summary>
public class MsfQuickBuildMoba
{
    /// <summary>
    /// Have in mind that if you change it, it might take "a while" 
    /// for the editor to pick up changes 
    /// </summary>
    public static string QuickSetupRoot = "Assets/Server";

    public static BuildTarget TargetPlatform = BuildTarget.StandaloneOSX;

    /// <summary>
    /// Build with "Development" flag, so that we can see the console if something 
    /// goes wrong
    /// </summary>
    public static BuildOptions BuildOptions = BuildOptions.Development;

    public static string PrevPath = null;

    [MenuItem("Tools/Msf/Build All", false, 0)]
    public static void BuildGame()
    {
        var path = GetPath();
        if (string.IsNullOrEmpty(path))
            return;

        BuildMaster(path);
        BuildSpawner(path);
        BuildClient(path);
        BuildGameServer(path);
    }

    /// <summary>
    /// Creates a build for master server and spawner
    /// </summary>
    /// <param name="path"></param>
    public static void BuildMaster(string path)
    {
        var masterScenes = new[]
        {
            QuickSetupRoot+ "/Scenes/Master.unity"
        };

        BuildPipeline.BuildPlayer(masterScenes, path + "/Master.app", TargetPlatform, BuildOptions);
    }

    /// <summary>
    /// Creates a build for master server and spawner
    /// </summary>
    /// <param name="path"></param>
    public static void BuildSpawner(string path)
    {
        var masterScenes = new[]
        {
            QuickSetupRoot+ "/Scenes/Spawner.unity"
        };

        BuildPipeline.BuildPlayer(masterScenes, path + "/Spawner.app", TargetPlatform, BuildOptions);
    }

    /// <summary>
    /// Creates a build for client
    /// </summary>
    /// <param name="path"></param>
    public static void BuildClient(string path)
    {
        var clientScenes = new[]
        {
            QuickSetupRoot+ "/Scenes/Client.unity",
            // Add all the game scenes
            QuickSetupRoot+ "/Scenes/GameLevels/OneVOne.unity"
        };
        BuildPipeline.BuildPlayer(clientScenes, path + "/Client.app", TargetPlatform, BuildOptions);
    }

    /// <summary>
    /// Creates a build for game server
    /// </summary>
    /// <param name="path"></param>
    public static void BuildGameServer(string path)
    {
        var gameServerScenes = new[]
        {
            QuickSetupRoot+"/Scenes/GameServer.unity",
            // Add all the game scenes
            QuickSetupRoot+"/Scenes/GameLevels/OneVOne.unity"
        };
        BuildPipeline.BuildPlayer(gameServerScenes, path + "/GameServer.app", TargetPlatform, BuildOptions);
    }

    #region Editor Menu

    [MenuItem("Tools/Msf/Build Master + Spawner", false, 11)]
    public static void BuildMasterAndSpawnerMenu()
    {
        var path = GetPath();
        if (!string.IsNullOrEmpty(path))
        {
            BuildMaster(path);
            BuildSpawner(path);
        }
    }

    [MenuItem("Tools/Msf/Build Client", false, 11)]
    public static void BuildClientMenu()
    {
        var path = GetPath();
        if (!string.IsNullOrEmpty(path))
        {
            BuildClient(path);
        }
    }

    [MenuItem("Tools/Msf/Build Game Server", false, 11)]
    public static void BuildGameServerMenu()
    {
        var path = GetPath();
        if (!string.IsNullOrEmpty(path))
        {
            BuildGameServer(path);
        }
    }

    #endregion

    public static string GetPath()
    {
        var prevPath = EditorPrefs.GetString("msf.buildPath", "");
        string path = EditorUtility.SaveFolderPanel("Choose Location for binaries", prevPath, "");

        if (!string.IsNullOrEmpty(path))
        {
            EditorPrefs.SetString("msf.buildPath", path);
        }
        return path;
    }
}