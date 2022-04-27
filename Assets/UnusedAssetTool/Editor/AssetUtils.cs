using System.Collections.Generic;
using UnityEditor;
using System.IO;

/*

- wanna edit the excludes start, contains and end, provide defaults ones and be able to reinitilize them

*/

public class AssetUtils
{
    public static string[] FindSceneAssets(IEnumerable<string> assets)
    {
        var sceneAssets = new List<string>();
        var enumerator = assets.GetEnumerator();
        while (enumerator.MoveNext())
        {
            if (Path.GetExtension(enumerator.Current) != SCENE_EXTENSION) continue;

            sceneAssets.Add(enumerator.Current);    
        }

        return sceneAssets.ToArray();
    }

    public static string[] GetAllFolders(string folderPath)
    {
        var folders = new List<string> { folderPath };
        for (int i = 0; i < folders.Count; i++)
        {
            folders.AddRange(AssetDatabase.GetSubFolders(folders[i]));
        }

        return folders.ToArray();
    }    

    private const string SCENE_EXTENSION = ".unity";
}