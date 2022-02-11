using UnityEngine;
using UnityEditor;

namespace MultiBuild
{
    public class MultiBuildSettings : ToolSettings<MultiBuildSettings> 
    {
        #region Exposed
        
        [Header("Build options")]
        [SerializeField] private BuildThread[] _threads;

        #endregion


        #region Properties

        public BuildThread[] Threads => _threads;
            
        #endregion


        #region Utils
        
        public string[] GetScenesPath(SceneAsset[] scenes)
        {
            var sceneNames = new string[scenes.Length];

            for (int i = 0; i < scenes.Length; i++)
            {
                var scene = scenes[i];
                var scenePath = AssetDatabase.GetAssetPath(scene);
                sceneNames[i] = scenePath;
            }

            return sceneNames;
        }

        #endregion
    }
}