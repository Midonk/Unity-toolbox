#if UNITY_EDITOR
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;

namespace MultiBuild
{
    [CreateAssetMenu(fileName = "Settings", menuName = "MultiBuild/Settings")]
    public class MultiBuildSettings : ToolSettings<MultiBuildSettings> 
    {
        #region Exposed
        
        [SerializeField, Header("Build options")]
        private BuildThread[] _threads;

        #endregion


        #region Public Fields

        internal InternalBuildThread[] threads;

        #endregion


        #region Unity API
        
        private void OnValidate() 
        {
            ConvertThreads();    
        }

        #endregion


        #region Utils
            
        private static MultiBuildSettings GetSettings()
        {
            return AssetDatabase.LoadAssetAtPath<MultiBuildSettings>(SETTINGSPATH);
        }

        private void ConvertThreads()
        {
            var tmpThreads = new List<InternalBuildThread>();

            foreach (var thread in _threads)
            {
                var tmpThread = new InternalBuildThread();
                tmpThread.activeThread = thread.activeThread;
                tmpThread.buildPath = $"{thread.localPathName}/{thread.buildName}.exe";
                tmpThread.scenesToBuild = GetScenesPath(thread.scenesToBuild);
                tmpThread.buildTarget = thread.buildTarget;
                tmpThread.buildOptions = thread.buildOptions;

                tmpThreads.Add(tmpThread);
            }

            threads = tmpThreads.ToArray();
        }

        private string[] GetScenesPath(SceneAsset[] scenes)
        {
            var scenesName = new List<string>();

            foreach (var scene in scenes)
            {
                var scenePath = AssetDatabase.GetAssetPath(scene);
                scenesName.Add(scenePath);
            }

            return scenesName.ToArray();
        }

        #endregion


        #region Private Fields

        private const string SETTINGSPATH = "Assets/Settings/MultiBuild_Tool/Settings/Settings.asset";

        #endregion
    }


    [System.Serializable]
    public class BuildThread
    {
        public string buildName = "myBuild";
        public bool activeThread = true;
        public SceneAsset[] scenesToBuild;
        [Tooltip("Path from the project root where your build will end up")]
        public string localPathName = "Builds";
        public BuildTarget buildTarget = BuildTarget.NoTarget;
        public BuildOptions buildOptions;
    }

    public class InternalBuildThread
    {
        public bool activeThread;
        public string buildPath;
        public string[] scenesToBuild;
        public BuildTarget buildTarget;
        public BuildOptions buildOptions;
    }
}
#endif