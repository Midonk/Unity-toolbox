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


        #region Main

        /// <summary>
        ///     Setup the panel and the buttons with the given parameters
        /// <summary>
        public void RebuildPanel(List<string> buttonPaths, string header)
        {
            var headerText = header.Length > 0 ? header : "Root";
            _headerTitle.text = headerText;
            foreach (Transform child in _uiContainer.transform)
            {
                Destroy(child.gameObject);
            }

            for (int i = 0; i < buttonPaths.Count; i++)
            {
                var button = BuildButton(buttonPaths[i]);
                _menuButtons.Add(button);
                if(i > 0) continue;

                Selectable selection = _uiContainer.GetChild(_uiContainer.childCount - 1).GetComponent<Selectable>();
                selection.Select();
            }
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


        #region Private

        private List<MenuButtonBase> _menuButtons = new List<MenuButtonBase>();

        #endregion
    }
}