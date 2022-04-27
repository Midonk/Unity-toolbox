using System.Collections.Generic;
using UnityEditor;
using UnityEngine;

public class UnusedAssetTool : MonoBehaviour
{
    [MenuItem("Tools/Unused assets/Generate report")]
    private static void GenerateReport()
    {
        var settings = UnusedAssetToolSettings.GetOrCreate();
        var assets = new List<string>(AssetDatabase.GetAllAssetPaths());

        AssetDatabase.DisallowAutoRefresh();

        DiscardExcludes(assets, settings);
        var folders = AssetUtils.GetAllFolders(ASSET_FOLDER_NAME);
        Discard(assets, folders);
        var scenes = AssetUtils.FindSceneAssets(assets);
        var usedAssets = new List<string>(AssetDatabase.GetDependencies(scenes, true));
        DiscardExcludes(usedAssets, settings);
        assets.Sort();

        var writer = new ReportWriter();
        var header = writer.GenerateHeader(assets.Count, folders.Length, scenes.Length, usedAssets.Count);
        var report = writer.GenerateBody(assets, usedAssets);
        writer.WriteReport(REPORT_PATH, report, header);

        AssetDatabase.AllowAutoRefresh();

        AssetDatabase.ImportAsset(REPORT_PATH);
    }

    [MenuItem("Tools/Unused assets/Select unused")]
    private static void SelectUnused()
    {
        AssetDatabase.DisallowAutoRefresh();

        if(!AssetDatabase.LoadAssetAtPath<Object>(REPORT_PATH))
        {
            GenerateReport();
        }

        var reader = new ReportReader();
        var assetPaths = reader.ReadReport(REPORT_PATH);
        var selection = new Object[assetPaths.Length];
        for (int i = 0; i < assetPaths.Length; i++)
        {
            selection[i] = AssetDatabase.LoadAssetAtPath<Object>(assetPaths[i]);
            EditorGUIUtility.PingObject(selection[i]);
        }
     
        AssetDatabase.AllowAutoRefresh();
        Selection.objects = selection;
    }

    private static void Discard(List<string> workables, string[] discards)
    {
        for (int i = 0; i < discards.Length; i++)
        {
            workables.Remove(discards[i]);
        }
    }

    //Discard every excluded patterns (ProjectSettings, Packages, .cs, .md, ...)
    private static void DiscardExcludes(List<string> assets, UnusedAssetToolSettings settings)
    {
        assets.RemoveAll(s => !s.StartsWith(ASSET_FOLDER_NAME));
        var startExcludes = settings.StartExcludes;
        var containsExcludes = settings.ContainExcludes;
        var endExcludes = settings.EndExcludes;

        for (int i = 0; i < startExcludes.Length; i++)
        {
            if(string.IsNullOrEmpty(startExcludes[i])) continue;

            assets.RemoveAll(s => s.StartsWith(startExcludes[i]));
        }

        for (int i = 0; i < endExcludes.Length; i++)
        {
            if(string.IsNullOrEmpty(endExcludes[i])) continue;

            assets.RemoveAll(s => s.EndsWith(endExcludes[i]));
        }
        
        for (int i = 0; i < containsExcludes.Length; i++)
        {
            if(string.IsNullOrEmpty(containsExcludes[i])) continue;

            assets.RemoveAll(s => s.Contains(containsExcludes[i]));
        }
    }

    private const string ASSET_FOLDER_NAME = "Assets";
    public const string REPORT_PATH = "Assets/UnusedAssetReport.txt";
}