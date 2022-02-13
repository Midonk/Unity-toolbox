using UnityEngine;
using UnityEditor;

namespace TF.MultiBuilder.Editor
{
    [System.Serializable]
    public struct BuildThread
    {
        [Tooltip("If false, this thread will be ignored when multibuilding")]
        public bool activeThread;
        [Tooltip("Name of your build, this will appears as the title of your player's window")]
        public string productName;
        [Tooltip("Path from the project root where your build will end up")]
        public string localBuildPath;
        [Tooltip("Platform settings that will be used to build your project")]
        public BuildTarget buildTarget;
        [Tooltip("Additional options to make your build match your purpose")]
        public BuildOptions buildOptions;
        [Tooltip("Scenes that will be built. Similar usage to scenes in Build Settings menu")]
        public SceneAsset[] scenesToBuild;
    }
}