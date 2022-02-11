using UnityEngine;
using UnityEditor;
using UnityEditor.Build.Reporting;
using System.Text;
using System;

namespace MultiBuild
{
    public class BuildManager
    {
        #region Main
        
        [MenuItem("Tools/Multibuild tool")]
        public static void MultiBuildProcess()
        {
            MultiBuildSettings buildSettings = MultiBuildSettings.GetOrCreate();
            if (buildSettings.Threads.Length == 0) return;

            _totalBuildTime = 0;
            _initialProductName = PlayerSettings.productName;
            _outputMessage.Clear();
            ProcessThreads(buildSettings);
        }

        #endregion


        #region Plumbery

        private static void ProcessThreads(MultiBuildSettings buildSettings)
        {
            Debug.Log($"MultiBuild : Start processing");
            var processedTreads = 0;
            foreach (var thread in buildSettings.Threads)
            {
                if (!thread.activeThread) continue;
                if (thread.scenesToBuild.Length == 0) continue;
                if (string.IsNullOrWhiteSpace(thread.localPathName)) continue;

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
            var buildPath = $"{thread.localPathName}/{thread.productName}";
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
                _outputMessage.AppendLine($"Build succeeded: '<color=cyan>{Application.dataPath}/{buildPath}</color>' built in {buildTime / 1000f : #0.##} seconds");
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
                    + $"Processed => <color=cyan>{processedTreads}</color> threads. See below for more infos\n"
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