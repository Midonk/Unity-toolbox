using UnityEngine;
using UnityEngine.SceneManagement;
using TF.DebugMenu.Core;

namespace TF.DebugMenu.Utils
{
    public class DebugSceneLoader : MonoBehaviour
    {
        #region Main

        public void LoadDebugScene(string sceneName)
        {
            DisplayMenu();
            if(SceneManager.GetSceneByName(sceneName).isLoaded) return;

            SceneManager.LoadSceneAsync(sceneName, LoadSceneMode.Additive);
        }

        public void DisplayMenu()
        {
            if(!DebugMenuHandler.Instance) return;
            
            DebugMenuHandler.Instance.DisplayMenu();
        }
                
        #endregion
    }
}

