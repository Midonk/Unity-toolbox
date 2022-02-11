using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

// Make link between root panel and menu buttons

namespace DebugMenu
{
    public partial class MenuPanel : MonoBehaviour
    {
        #region Exposed

        [Header("Config")]
        [SerializeField] private Text _headerText;
        [SerializeField] private Transform _buttonContainer;
        
        [Header("Buton types")]
        [SerializeField] private MenuButtonLink[] _buttonLinks;
        [SerializeField] private MenuNavigationButton _navigationButton;

        #endregion

        private string CurrentPath
        {
            get
            {
                if(string.IsNullOrEmpty(_currentPath)) return ROOTPATH;

                return _currentPath;
            }

            set => _currentPath = value;
        }

        private void Awake() 
        {
            for (int i = 0; i < _buttonLinks.Length; i++)
            {
                var button = _buttonLinks[i];
                _buttonDisplays.Add(button.ButtonType, button.ButtonDisplay);
            }    
        }

        #region Main

        /// <summary>
        ///     Setup the panel and the buttons with the given parameters
        /// <summary>
        public void BuildPanel(string[] buttonPaths)
        {
            HidePanelButtons();
            var parentPath = StringUtils.GetParentPath(buttonPaths[0], '/');
            if(_buttons.ContainsKey(parentPath))
            {
                Debug.LogWarning($"Something is wrong, the buttons for <color=orange>{parentPath}</color> exists but a build for this same path has been requested");
                DisplayPanelButtons(parentPath);
                return;
            }

            var buttons = new MenuButtonBase[buttonPaths.Length];
            for (int i = 0; i < buttonPaths.Length; i++)
            {
                buttons[i] = BuildButton(buttonPaths[i]);
            }

            _buttons.Add(parentPath, buttons);
            
            CurrentPath = parentPath;
            _headerText.text = CurrentPath;
            SelectButton();
        }

        private void SelectButton()
        {
            Selectable buttonToSelect = _buttons[_currentPath][0];
            buttonToSelect.Select();
        }

        private void HidePanelButtons()
        {
            if(_buttons.Count == 0) return;

            var buttons = _buttons[_currentPath];
            for (int i = 0; i < buttons.Length; i++)
            {
                buttons[i].gameObject.SetActive(false);
            }
        }

        public void DisplayPanelButtons(string path)
        {
            if(_buttons.Count == 0) return;

            HidePanelButtons();
            var buttons = _buttons[path];
            for (int i = 0; i < buttons.Length; i++)
            {
                buttons[i].gameObject.SetActive(true);
            }

            CurrentPath = path;
            _headerText.text = CurrentPath;
            SelectButton();
        }

        private MenuButtonBase BuildButton(string path)
        {
            var buttonPrefab = !DebugAttributeRegistry.HasKey(path) ? _navigationButton : DefineDisplay(path);
            var button = Instantiate(buttonPrefab, _buttonContainer);
            button.Build(path);

            return button;
        }

        private MenuButtonBase DefineDisplay(string path)
        {
            var buttonType = $"{DebugAttributeRegistry.GetAttribute(path).GetType().Name}";
            foreach (var button in _buttonDisplays)
            {
                var linkedType = button.Key;
                if(!buttonType.Equals(linkedType)) continue;

                return button.Value;
            }

            Debug.LogError($"It seems you forgot to provide the <color=red>{buttonType}</color> type a display to build your <color=red>Debug Menu</color>", this);
            throw new System.NotSupportedException();
        }

        #endregion


        #region Utils

        public bool HasButtons(string path) => _buttons.ContainsKey(path);
            
        #endregion


        #region Private

        private List<MenuButtonBase> _menuButtons = new List<MenuButtonBase>();
        private string _currentPath;
        private Dictionary<string, MenuButtonBase[]> _buttons = new Dictionary<string, MenuButtonBase[]>();
        private Dictionary<string, MenuButtonBase> _buttonDisplays = new Dictionary<string, MenuButtonBase>();
        private const string ROOTPATH = "Root";

        #endregion
    }

}