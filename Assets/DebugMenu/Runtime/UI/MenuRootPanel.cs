using System.Collections.Generic;
using UnityEngine;

// Provides paths to the display

namespace DebugMenu
{
    public class MenuRootPanel : MonoBehaviour
    {
        #region Exposed

        [SerializeField] private MenuPanel _menuPanel;

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

            var path = StringUtils.GetParentPath(_currentPath, SEPARATOR);
            ChangePanel(path);
        }

        /// <summary>
        ///     Go deeper into the panel hierarchy
        /// <summary>
        public void ChangePanel(string path)
        {
            _currentPath = path;
            if(_menuPanel.HasButtons(_currentPath))
            {
                _menuPanel.DisplayPanelButtons(_currentPath);
                return;
            }

            RefreshPaths(_currentPath);
        }

        public void DisplayMenu()
        {
            gameObject.SetActive(true);
        }

        private void HideMenu()
        {
            gameObject.SetActive(false);
        }

        #endregion


        #region Utils

        private void RefreshPaths(string comparingPath)
        {
            _methodPaths.Clear();
            var paths = DebugAttributeRegistry.Paths;
            var testedRoots = new List<string>();
            string separator = string.IsNullOrEmpty(_currentPath) ? "" : "/";
            foreach (var path in paths)
            {
                if(!path.StartsWith(comparingPath)) continue;
                
                //"truc/machin"
                var root = path.Remove(0, comparingPath.Length + separator.Length);
                var separatorIndex = root.IndexOf(SEPARATOR);
                if(separatorIndex > -1)
                {
                    root = root.Remove(separatorIndex);
                }

                if(testedRoots.Contains(root)) continue;

                testedRoots.Add(root);
                _methodPaths.Add($"{comparingPath}{separator}{root}");                
            }

            if(_methodPaths.Count == 0)
            {
                Debug.LogError($"'<color=red>{comparingPath}</color>' doesn't lead to any subfolder.");
                return;
            }

            _menuPanel.BuildPanel(_methodPaths.ToArray());
        }
             
        #endregion


        #region Private

        private static MenuRootPanel _instance;
        private string _currentPath = string.Empty;
        private List<string> _methodPaths = new List<string>();
        private const char SEPARATOR = '/';

        #endregion
    }
}