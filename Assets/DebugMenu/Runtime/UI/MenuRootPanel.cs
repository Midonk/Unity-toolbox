using System.Collections.Generic;
using UnityEngine;

// Provides paths to the display and request rebuilds

namespace DebugMenu
{
    public class MenuRootPanel : MonoBehaviour
    {
        #region Exposed

        [SerializeField] private MenuPanel _menuPanel;
        [SerializeField] private bool _debugMode;

        #endregion
    

        #region Public Properties

        public static MenuRootPanel Instance => _instance;
             
        #endregion


        #region Unity API

        private void Awake()
        {
            _instance = this;
            RefreshPaths(_currentPath);
        }

        #endregion


        #region Main

        /// <summary>
        ///     Return to the previous panel or close it if on root
        /// <summary>
        public void Return()
        {
            if (string.IsNullOrEmpty(_currentPath))
            {
                HideMenu();
                return;
            }

            var nameIndex = _currentPath.LastIndexOf(SEPARATOR);
            if(nameIndex < 0)
            {
                nameIndex = 0; 
            }

            _currentPath = _currentPath.Remove(nameIndex);
            RefreshPaths(_currentPath);         
        }

        /// <summary>
        ///     Go deeper into the panel hierarchy
        /// <summary>
        public void ChangePanel(string buttonPath)
        {
            _currentPath = buttonPath;
            RefreshPaths(_currentPath);
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


        #region Utils

        private void RefreshPaths(string comparingPath)
        {
            Debug.Log($"comparing path: <color=cyan>{comparingPath}</color>");
            _methodPaths.Clear();
            var paths = DebugAttributeRegistry.Paths;
            var testedRoots = new List<string>();
            string separator = string.IsNullOrEmpty(_currentPath) ? "" : "/";
            foreach (var path in paths)
            {
                if(!path.StartsWith(comparingPath)) continue;
                
                //"truc/machin"
                var root = path.Remove(0, comparingPath.Length + separator.Length);
                var endNameIndex = root.IndexOf(SEPARATOR);
                if(endNameIndex > -1)
                {
                    root = root.Remove(endNameIndex);
                }

                if(testedRoots.Contains(root)) continue;

                testedRoots.Add(root);
                _methodPaths.Add($"{comparingPath}{separator}{root}");                
                Debug.Log($"button path: <color=cyan>{comparingPath}{separator}{root}</color>");
            }

            if(_methodPaths.Count == 0)
            {
                Debug.LogError($"'<color=red>{comparingPath}</color>' doesn't lead to any subfolder.");
                return;
            }

            _menuPanel.RebuildPanel(_methodPaths, _currentPath);
        }
             
        #endregion


        #region Private

        private static MenuRootPanel _instance;
        private string _currentPath = "";
        private List<string> _methodPaths = new List<string>();
        private const char SEPARATOR = '/';

        #endregion
    }
}