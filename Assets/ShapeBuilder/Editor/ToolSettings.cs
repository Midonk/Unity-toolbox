using UnityEngine;
using UnityEditor;
using System.IO;

public abstract class ToolSettings<T> : ScriptableObject where T : ScriptableObject
{    
    public static T GetOrCreate()
    {
        var settings = GetSettings();

        if (settings is null)
        {
            settings = CreateSettings();            
            Debug.LogWarning($"{typeof(T)} : New settings created for '<color=cyan>{typeof(T)}</color>' tool at \r" +
                              $"'<color=cyan>{SETTINGSPATH}</color>'");
        }

        return settings;
    }

    private static T GetSettings()
    {
        return AssetDatabase.LoadAssetAtPath<T>($"{SETTINGSPATH}/{typeof(T)}.asset");
    }

    private static T CreateSettings()
    {
        if (!Directory.Exists(SETTINGSPATH))
        {
            Debug.Log("Create directory");
            Directory.CreateDirectory(SETTINGSPATH);
            AssetDatabase.Refresh();
        }

        Debug.Log("Create settings");
        var settings = ScriptableObject.CreateInstance<T>();
        AssetDatabase.CreateAsset(settings, $"{SETTINGSPATH}/{typeof(T)}.asset");
        AssetDatabase.SaveAssets();

        return settings;
    }

    private const string SETTINGSPATH = "Assets/Editor/Settings";
}