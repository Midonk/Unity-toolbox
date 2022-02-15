using UnityEngine;
using UnityEditor;
using UnityEditor.Build.Reporting;
using System.Text;
using System;
using System.IO;

namespace TF.MultiBuilder.Editor
{
    public class BuildManager
    {
        #region Main
        
        [MenuItem("Tools/Multibuilder/Build")]
        public static void MultiBuildProcess()
        {
            MultiBuildSettings buildSettings = MultiBuildSettings.GetOrCreate();
            if (buildSettings.Threads.Length == 0)
            {
                Debug.Log($"There is actually <color=orange>no thread to build</color>. To add a thread, go to the settings asset (<color=orange>{MultiBuildSettings.SettingAssetPath}</color>)", buildSettings);
                return;
            }

            _totalBuildTime = 0;
            _initialProductName = PlayerSettings.productName;
            _outputMessage.Clear();
            ProcessThreads(buildSettings);
        }

        [MenuItem("Tools/Multibuilder/Show Settings")]
        public static void PingSettings()
        {
            var settings = MultiBuildSettings.GetOrCreate();
            EditorGUIUtility.PingObject(settings);
        }

        #endregion


        #region Plumbery

        private static void ProcessThreads(MultiBuildSettings buildSettings)
        {
            Debug.Log($"MultiBuild : Start processing");
            var processedTreads = 0;
            var threads = buildSettings.Threads;
            for (int i = 0; i < threads.Length; i++)
            {
                var thread = threads[i];
                if (!thread.activeThread) continue;
                if (thread.scenesToBuild.Length == 0)
                {
                    Debug.LogWarning($"Thread n° <color=orange>{i}</color> has <color=orange>no scene</color> referenced. This thread will be ignored", buildSettings);
                    continue;
                }

                if (!Directory.Exists(thread.buildPath))
                {
                    Debug.LogWarning($"Thread n° <color=orange>{i}</color> has <color=orange> invalid build path</color>. This thread will be ignored", buildSettings);
                    continue;
                }

                try
                {
                    Build(thread);
                }

                catch (System.Exception)
                {
                    EndProcess(processedTreads);
                    throw;
                }

                processedTreads++;
            }

            EndProcess(processedTreads);
        }

        private static void Build(BuildThread thread)
        {
            PlayerSettings.productName = thread.productName;
            var settings = MultiBuildSettings.GetOrCreate();
            BuildPlayerOptions buildPlayerOptions = new BuildPlayerOptions();
            var scenesToBuild = settings.GetScenesPath(thread.scenesToBuild);
            buildPlayerOptions.scenes = scenesToBuild;
            var buildPath = $"{thread.buildPath}/{thread.productName}";
            buildPlayerOptions.locationPathName = buildPath;
            buildPlayerOptions.target = thread.buildTarget;
            buildPlayerOptions.options = thread.buildOptions;
            var currentStartTime = DateTime.Now.Millisecond;

            BuildReport report = BuildPipeline.BuildPlayer(buildPlayerOptions);
            BuildSummary summary = report.summary;

            if (summary.result == BuildResult.Succeeded)
            {
                var buildTime = summary.totalTime.Milliseconds;
                _totalBuildTime += buildTime;
                _outputMessage.AppendLine($"Build succeeded: '<color=cyan>{buildPath}</color>' built in {buildTime / 1000f : #0.##} seconds");
            }

            else if (summary.result == BuildResult.Failed)
            {
                var errorCount = summary.totalErrors;
                _outputMessage.AppendLine($"Build failed: <color=red>{thread.productName}</color> failed to build\n"
                                        + $"<color=red>{summary.totalErrors}</color> error{(errorCount > 1 ? "s" : "")} have been raized");
            }
        }

        private static void EndProcess(int processedTreads)
        {
            PlayerSettings.productName = _initialProductName;
            Debug.Log($"MultiBuild : End of process\n"
                    + $"Processed <color=cyan>{processedTreads}</color> threads. See below for more infos\n"
                    + $"{_outputMessage.ToString()}"
                    + $"All threads built in <color=cyan>{_totalBuildTime / 1000f : #0.##}</color> seconds\n");
        }
            
        #endregion


        #region Private Fields

        private static StringBuilder _outputMessage = new StringBuilder();
        private static string _initialProductName;
        private static float _totalBuildTime;
        
        #endregion
    }
}