using System.IO;
using UnityEditor;
using UnityEngine;

public class CreateEntityFolders : Editor
{
    static readonly string[] floderNames = { "Aspects", "Bakers", "Components", "Jobs", "Systems", "SystemGroups" };
    [MenuItem("Assets/Create/Entities/Entity Folders", false, -1000)]
    static void CreateFolders()
    {
        var dataPath = Application.dataPath;
        var selectPath = AssetDatabase.GetAssetPath(Selection.activeObject);
        selectPath = selectPath.Replace("Assets", "");
        var path = dataPath + selectPath;
        foreach (var floderName in floderNames)
            Directory.CreateDirectory(path + "/" + floderName);
        AssetDatabase.Refresh();
    }
}