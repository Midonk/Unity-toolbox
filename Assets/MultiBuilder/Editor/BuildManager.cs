#if UNITY_EDITOR
using UnityEngine;
using UnityEditor;
using UnityEditor.Build.Reporting;

namespace MultiBuild
{
    public class BuildManager : MonoBehaviour
    {
        #region Main
            
        [MenuItem("Build/Multibuild tool")]
        public static void MultiBuildProcess()
        {
            MultiBuildToolSetting buildSettings = MultiBuildToolSetting.GetOrCreate();

            foreach (var thread in buildSettings.threads)
            {
                if(!thread.activeThread) continue;
                Build(thread);
            }

            Debug.Log($"MultiBuild : End of process");
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
                var buildTime = summary.totalTime.TotalSeconds;
                Debug.Log($"Build succeeded: '{thread.buildPath}' built in {buildTime : #0.##} seconds");
            }

            if (summary.result == BuildResult.Failed)
            {
                var errorCount = summary.totalErrors;
                Debug.Log($"Build failed: {summary.totalErrors} error{(errorCount > 1 ? "s" : "")} have been raized");
            }
        }

        #endregion
    }
}

#endif