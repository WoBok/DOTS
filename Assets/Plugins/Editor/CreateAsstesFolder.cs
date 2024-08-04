using System.IO;
using UnityEditor;
using UnityEngine;

public class CreateAsstesFolder : Editor
{
    [MenuItem("Assets/Create/Assets Folder/Shaders", false, -999)]
    static void CreateShadersFolder()
    {
        var dataPath = Application.dataPath;
        var selectPath = AssetDatabase.GetAssetPath(Selection.activeObject);
        selectPath = selectPath.Replace("Assets", "");
        var path = dataPath + selectPath;
        Directory.CreateDirectory(path + "/" + "Shaders");
        AssetDatabase.Refresh();
    }
    [MenuItem("Assets/Create/Assets Folder/Scripts", false, -998)]
    static void CreateScriptsFolder()
    {
        var dataPath = Application.dataPath;
        var selectPath = AssetDatabase.GetAssetPath(Selection.activeObject);
        selectPath = selectPath.Replace("Assets", "");
        var path = dataPath + selectPath;
        Directory.CreateDirectory(path + "/" + "Scripts");
        AssetDatabase.Refresh();
    }
    [MenuItem("Assets/Create/Assets Folder/Scenes", false, -997)]
    static void CreateScenesFolder()
    {
        var dataPath = Application.dataPath;
        var selectPath = AssetDatabase.GetAssetPath(Selection.activeObject);
        selectPath = selectPath.Replace("Assets", "");
        var path = dataPath + selectPath;
        Directory.CreateDirectory(path + "/" + "Scenes");
        AssetDatabase.Refresh();
    }
    [MenuItem("Assets/Create/Assets Folder/Materials", false, -996)]
    static void CreateMaterialsFolder()
    {
        var dataPath = Application.dataPath;
        var selectPath = AssetDatabase.GetAssetPath(Selection.activeObject);
        selectPath = selectPath.Replace("Assets", "");
        var path = dataPath + selectPath;
        Directory.CreateDirectory(path + "/" + "Materials");
        AssetDatabase.Refresh();
    }
    [MenuItem("Assets/Create/Assets Folder/Prefabs", false, -995)]
    static void CreatePrefabsFolder()
    {
        var dataPath = Application.dataPath;
        var selectPath = AssetDatabase.GetAssetPath(Selection.activeObject);
        selectPath = selectPath.Replace("Assets", "");
        var path = dataPath + selectPath;
        Directory.CreateDirectory(path + "/" + "Prefabs");
        AssetDatabase.Refresh();
    }
    [MenuItem("Assets/Create/Assets Folder/Textures", false, -994)]
    static void CreateTexturesFolder()
    {
        var dataPath = Application.dataPath;
        var selectPath = AssetDatabase.GetAssetPath(Selection.activeObject);
        selectPath = selectPath.Replace("Assets", "");
        var path = dataPath + selectPath;
        Directory.CreateDirectory(path + "/" + "Textures");
        AssetDatabase.Refresh();
    }
    [MenuItem("Assets/Create/Assets Folder/Models", false, -993)]
    static void CreateModelsFolder()
    {
        var dataPath = Application.dataPath;
        var selectPath = AssetDatabase.GetAssetPath(Selection.activeObject);
        selectPath = selectPath.Replace("Assets", "");
        var path = dataPath + selectPath;
        Directory.CreateDirectory(path + "/" + "Models");
        AssetDatabase.Refresh();
    }
    [MenuItem("Assets/Create/Assets Folder/Animations", false, -992)]
    static void CreateAnimationsFolder()
    {
        var dataPath = Application.dataPath;
        var selectPath = AssetDatabase.GetAssetPath(Selection.activeObject);
        selectPath = selectPath.Replace("Assets", "");
        var path = dataPath + selectPath;
        Directory.CreateDirectory(path + "/" + "Animations");
        AssetDatabase.Refresh();
    }
    [MenuItem("Assets/Create/Assets Folder/Audio", false, -991)]
    static void CreateAudioFolder()
    {
        var dataPath = Application.dataPath;
        var selectPath = AssetDatabase.GetAssetPath(Selection.activeObject);
        selectPath = selectPath.Replace("Assets", "");
        var path = dataPath + selectPath;
        Directory.CreateDirectory(path + "/" + "Audio");
        AssetDatabase.Refresh();
    }
    [MenuItem("Assets/Create/Assets Folder/Resources", false, -990)]
    static void CreateResourcesFolder()
    {
        var dataPath = Application.dataPath;
        var selectPath = AssetDatabase.GetAssetPath(Selection.activeObject);
        selectPath = selectPath.Replace("Assets", "");
        var path = dataPath + selectPath;
        Directory.CreateDirectory(path + "/" + "Resources");
        AssetDatabase.Refresh();
    }
    [MenuItem("Assets/Create/Assets Folder/Plugins", false, -989)]
    static void CreatePluginsFolder()
    {
        var dataPath = Application.dataPath;
        var selectPath = AssetDatabase.GetAssetPath(Selection.activeObject);
        selectPath = selectPath.Replace("Assets", "");
        var path = dataPath + selectPath;
        Directory.CreateDirectory(path + "/" + "Plugins");
        AssetDatabase.Refresh();
    }
    [MenuItem("Assets/Create/Assets Folder/All", false, -970)]
    static void CreateAllFolder()
    {
        CreateShadersFolder();
        CreateScriptsFolder();
        CreateScenesFolder();
        CreateMaterialsFolder();
        CreatePrefabsFolder();
        CreateTexturesFolder();
        CreateModelsFolder();
        CreateAnimationsFolder();
        CreateAudioFolder();
        CreateResourcesFolder();
        CreatePluginsFolder();
    }
}