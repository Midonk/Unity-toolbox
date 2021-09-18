using System.Collections.Generic;
using UnityEngine;

namespace DebugMenu
{
    public class MenuRootPanel : MonoBehaviour
    {
        #region Exposed

        [SerializeField]
        private MenuPanel _menuPanel;

        #endregion
    

        #region Public Properties

        public static MenuRootPanel Instance => _instance;
             
        #endregion


        #region Unity API

        private void OnGUI() 
        {
            foreach (var button in GetCurrentButtons(_currentPath))
            {
                if(GUILayout.Button($"{button}"))
                {
                    ChangePanel(button);
                }
            }    

            if(string.IsNullOrEmpty(_currentPath)) return;

            if(GUILayout.Button($"Return"))
            {
                Return();
            }
        }

        private void Awake()
        {
            _instance = this;
            var buttons = GetCurrentButtons(_currentPath);
            //_menuPanel.RebuildPanel(buttons, _currentPath);
        }

        #endregion


        #region Main

        private string[] GetCurrentButtons(string comparingPath)
        {
            var paths = DebugAttributeRegistry.Paths;
            var buttonNames = new List<string>();
            foreach (var path in paths)
            {
                if(!path.Contains(comparingPath)) continue;
                //this means there is a method to invoke here VVVV
                if(path.Length == comparingPath.Length)
                {
                    DebugAttributeRegistry.InvokeMethod(path);
                    return null;
                }

                var charToRemove = comparingPath.Length;
                var truncatedPath = path.Remove(0, charToRemove);
                var name = truncatedPath.Split('/')[0];
                if(buttonNames.Contains(name)) continue;
                
                buttonNames.Add(name);
            }

            return buttonNames.ToArray();
        }

        internal void Return()
        {
            RevertCurrentPath();            
            var buttons = GetCurrentButtons(_currentPath);
            //_menuPanel.RebuildPanel(buttons, _currentPath);

            Debug.Log($"Return to '<color=cyan>{(_currentPath.Length > 0 ? _currentPath : "Root")}</color>'");
        }

        public void ChangePanel(string buttonName)
        {
            var buttons = GetCurrentButtons(_currentPath + buttonName);
            if(buttons == null) return;

            _currentPath += $"{buttonName}/";
            if(buttons.Length == 0)
            {
                Debug.LogError($"<color=red>Debug menu: Oups ! It seems the path '<color=cyan>{_currentPath}</color>' doesn't exists</color>");
                return;
            }

            //_menuPanel.RebuildPanel(buttons, _currentPath);
        }

        private void RevertCurrentPath()
        {
            var splittedPath = _currentPath.Split('/');
            var depth = splittedPath.Length;
            var charToRemove = splittedPath[depth - 2].Length + 1;

            _currentPath = _currentPath.Remove(_currentPath.Length - charToRemove, charToRemove);
        }

        public void DisplayMenu()
        {
            _menuPanel.gameObject.SetActive(true);
        }

        private void HideMenu()
        {
            _menuPanel.gameObject.SetActive(false);
        }

        #endregion 



        #region Private

        private static MenuRootPanel _instance;
        private string _currentPath = "";

        #endregion
    }
}