using UnityEngine;
using UnityEngine.SceneManagement;

namespace DebugMenu
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
            if(!MenuRootPanel.Instance) return;
            
            MenuRootPanel.Instance.DisplayMenu();
        }
             
        #endregion
    }
}
