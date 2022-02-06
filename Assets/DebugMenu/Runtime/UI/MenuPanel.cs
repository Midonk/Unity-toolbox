using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

// Make link between root panel and menu buttons

namespace DebugMenu
{
    public class MenuPanel : MonoBehaviour
    {
        #region Exposed

        [Header("Config")]
        [SerializeField] private Text _headerTitle;
        [SerializeField] private Transform _uiContainer;
        
        [Header("Buton types")]
        [SerializeField] private MenuNavigationButton _navigationButton;
        [SerializeField] private MenuVoidButton _voidButton;
        [SerializeField] private MenuBoolButton _boolButton;
        [SerializeField] private MenuNumberButton _numberButton;
        [SerializeField] private MenuSliderButton _sliderButton;
        [SerializeField] private MenuEnumButton _enumButton;

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
            _headerTitle.text = CurrentPath;
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
            _headerTitle.text = CurrentPath;
            SelectButton();
        }

        private MenuButtonBase BuildButton(string path)
        {
            var buttonPrefab = !DebugAttributeRegistry.HasKey(path) ? _navigationButton : DefineDisplay(path);
            var button = Instantiate(buttonPrefab, _uiContainer);
            button.Build(path);

            return button;
        }

        private MenuButtonBase DefineDisplay(string path)
        {
            var buttonType = $"{DebugAttributeRegistry.GetParameterType(path)}";
            if(buttonType.Contains("Int") || buttonType.Contains("Single")) return _numberButton;
            if(buttonType.Contains("Bool")) return _boolButton;
            if(string.IsNullOrEmpty(buttonType)) return _voidButton;
            return _enumButton;
        }

        #endregion


        #region Utils

        public bool HasButtons(string path) => _buttons.ContainsKey(path);
            
        #endregion


        #region Private

        private List<MenuButtonBase> _menuButtons = new List<MenuButtonBase>();
        private string _currentPath;
        private Dictionary<string, MenuButtonBase[]> _buttons = new Dictionary<string, MenuButtonBase[]>();
        private const string ROOTPATH = "Root";

        #endregion
    }
}