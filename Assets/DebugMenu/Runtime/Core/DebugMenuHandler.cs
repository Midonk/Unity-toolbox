using TF.DebugMenu.Buttons;
using UnityEngine;
using UnityEngine.UI;

namespace TF.DebugMenu.Core
{
    public class DebugMenuHandler : MonoBehaviour
    {
        #region Exposed

        [Header("Config")]
        [SerializeField] private Text _headerText;
        [SerializeField] private Transform _buttonContainer;
        
        [Header("Buton types")]
        [Tooltip("Link an attribute with its UI representation")]
        [SerializeField] private MenuButtonLink[] _buttonLinks;
        [SerializeField] private MenuNavigationButton _navigationButton;

        #endregion

        
        #region Unity API

        private void Awake() 
        {
            _instance = this;
            _menuBuilder = new MenuBuilder(_buttonLinks, _headerText, _buttonContainer, _navigationButton);
            _pathProcessor = new PathProcessor();
        }

        private void Start() 
        {
            RequestBuild();
        }
            
        #endregion


        #region Properties

        public static DebugMenuHandler Instance => _instance;
        
        #endregion


        #region Main            

        /// <summary>
        /// Return to the previous panel or close the menu if on root
        /// <summary>
        public void Return()
        {
            if (string.IsNullOrEmpty(_currentPath))
            {
                HideMenu();
                return;
            }

            var path = _pathProcessor.GetParentPath(_currentPath);
            ChangePanel(path);
        }

        /// <summary>
        /// Go deeper into the panel hierarchy
        /// <summary>
        public void ChangePanel(string path)
        {
            _currentPath = path;
            if(_menuBuilder.HasButtons(_currentPath))
            {
                _menuBuilder.DisplayPanelButtons(_currentPath);
                return;
            }

            RequestBuild();
        }

        #endregion

        
        #region Plumbery

        private void RequestBuild()
        {
             var paths = _pathProcessor.FetchPaths(_currentPath);
             _menuBuilder.BuildPanel(paths);
        }
        
        private void HideMenu()
        {
            gameObject.SetActive(false);
        }
            
        #endregion


        #region Utils

        public void DisplayMenu()
        {
            gameObject.SetActive(true);
        }
            
        #endregion


        #region Private Fields

        private MenuBuilder _menuBuilder;
        private PathProcessor _pathProcessor;
        private string _currentPath = string.Empty;
        private static DebugMenuHandler _instance;

        #endregion
    }
}