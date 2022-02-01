using UnityEngine;
using UnityEngine.SceneManagement;

// Load or unload debug menu scene

namespace DebugMenu
{
    public class MenuSceneHandler : MonoBehaviour
    {
        #region Main

        public void LoadSceneAdditive(string sceneName)
        {
            if (isLoaded) return;
            
            SceneManager.LoadSceneAsync(sceneName, LoadSceneMode.Additive);
            isLoaded = true;
        }

        public void RemoveAdditiveScene(string sceneName)
        {
            if (!isLoaded) return;
            
            SceneManager.UnloadSceneAsync(sceneName);
            isLoaded = false;
        }

        #endregion


        #region private

        private bool isLoaded;

        #endregion
    }
}