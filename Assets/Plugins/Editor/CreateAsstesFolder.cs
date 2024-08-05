using System.IO;
using UnityEditor;
using UnityEngine;

public class CreateAsstesFolder : Editor
{
    static void CreateDirectory(string folderName)
    {
        var dataPath = Application.dataPath;
        var selectPath = AssetDatabase.GetAssetPath(Selection.activeObject);
        selectPath = selectPath.Replace("Assets", "");
        var path = dataPath + selectPath;
        Directory.CreateDirectory($"{path}/{folderName}");
        AssetDatabase.Refresh();
    }
    [MenuItem("Assets/Create/Assets Folder/Shaders", false, -999)]
    static void CreateShadersFolder()
    {
        CreateDirectory("Shaders");
    }
    [MenuItem("Assets/Create/Assets Folder/Scripts", false, -998)]
    static void CreateScriptsFolder()
    {
        CreateDirectory("Scripts");
    }
    [MenuItem("Assets/Create/Assets Folder/Scenes", false, -997)]
    static void CreateScenesFolder()
    {
        CreateDirectory("Scenes");
    }
    [MenuItem("Assets/Create/Assets Folder/Materials", false, -996)]
    static void CreateMaterialsFolder()
    {
        CreateDirectory("Materials");
    }
    [MenuItem("Assets/Create/Assets Folder/Prefabs", false, -995)]
    static void CreatePrefabsFolder()
    {
        CreateDirectory("Prefabs");
    }
    [MenuItem("Assets/Create/Assets Folder/Textures", false, -994)]
    static void CreateTexturesFolder()
    {
        CreateDirectory("Textures");
    }
    [MenuItem("Assets/Create/Assets Folder/Models", false, -993)]
    static void CreateModelsFolder()
    {
        CreateDirectory("Models");
    }
    [MenuItem("Assets/Create/Assets Folder/Animations", false, -992)]
    static void CreateAnimationsFolder()
    {
        CreateDirectory("Animations");
    }
    [MenuItem("Assets/Create/Assets Folder/Audio", false, -991)]
    static void CreateAudioFolder()
    {
        CreateDirectory("Audio");
    }
    [MenuItem("Assets/Create/Assets Folder/Resources", false, -990)]
    static void CreateResourcesFolder()
    {
        CreateDirectory("Resources");
    }
    [MenuItem("Assets/Create/Assets Folder/Plugins", false, -989)]
    static void CreatePluginsFolder()
    {
        CreateDirectory("Plugins");
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