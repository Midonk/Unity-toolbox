#if UNITY_EDITOR
using UnityEngine;
using UnityEditor;
using UnityEditor.Build.Reporting;

namespace MultiBuild
{
    public class BuildManager : MonoBehaviour
    {
        #region Main
            
        [MenuItem("Tools/Multibuild tool")]
        public static void MultiBuildProcess()
        {
            Debug.Log($"MultiBuild : Start processing");
            MultiBuildSettings buildSettings = MultiBuildSettings.GetOrCreate();
            var processedTreads = 0;
            if(buildSettings.threads is null) return;

            foreach (var thread in buildSettings.threads)
            {
                if(!thread.activeThread) continue;
                if(thread.scenesToBuild.Length == 0) continue;

                Build(thread);
                processedTreads ++;
            }

            Debug.Log($"MultiBuild : End of process \n"
                    + $"Processed => <color=cyan>{processedTreads}</color> threads");
        }

        private static void Build(InternalBuildThread thread)
        {
            BuildPlayerOptions buildPlayerOptions = new BuildPlayerOptions();
            buildPlayerOptions.scenes = thread.scenesToBuild;
            buildPlayerOptions.locationPathName = thread.buildPath;
            buildPlayerOptions.target = thread.buildTarget;
            buildPlayerOptions.options = thread.buildOptions;

            BuildReport report = BuildPipeline.BuildPlayer(buildPlayerOptions);
            BuildSummary summary = report.summary;

            if (summary.result == BuildResult.Succeeded)
            {
                //var buildTime = (summary.buildEndedAt - summary.buildStartedAt).Milliseconds / 1000f;
                //Debug.Log($"Build succeeded: '{thread.buildPath}' built in {buildTime : #0.##} seconds");
            }

            if (summary.result == BuildResult.Failed)
            {
                var errorCount = summary.totalErrors;
                Debug.Log($"Build failed: <color=red>{summary.totalErrors}</color> error{(errorCount > 1 ? "s" : "")} have been raized");
            }
        }

        #endregion
    }
}

#endif