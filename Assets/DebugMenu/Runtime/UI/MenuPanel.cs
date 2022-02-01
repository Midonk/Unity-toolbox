using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;

namespace DebugMenu
{
    public class MenuPanel : MonoBehaviour
    {
        #region Exposed

        [SerializeField] private Text _headerTitle;
        [SerializeField] private RectTransform _buttonContainer;
        [SerializeField] private MenuButton _buttonPrefab;

        #endregion  


        #region Main

        /// <summary>
        ///     Setup the panel and the buttons with the given parameters
        /// <summary>
        public void RebuildPanel(string[] buttonNames, string header)
        {
            CheckButtonPool(buttonNames.Length);
            PrepareButtons(buttonNames);
            var headerText = header.Length > 0 ? header : "Root";
            _headerTitle.text = headerText;
        }

        #endregion


        #region Utils

        /// <summary>
        ///     Configure the panel's buttons
        /// <summary>
        private void PrepareButtons(string[] buttonNames)
        {
            for (int i = 0; i < buttonNames.Length; i++)
            {
                _menuButtons[i].ReferenceName = buttons[i];
                _menuButtons[i].Text = buttons[i];
                _menuButtons[i].gameObject.SetActive(true);
            }

            for (int i = buttonNames.Length; i < _menuButtons.Count; i++)
            {
                _menuButtons[i].gameObject.SetActive(false);
            }

            EventSystem.current.SetSelectedGameObject(_menuButtons[0].gameObject);
        }

        /// <summary>
        ///     Make sure there is enough buttons available to welcome the incoming panel
        /// <summary>
        private void CheckButtonPool(int neededButtons)
        {
            if(_menuButtons.Count >= neededButtons) return;
            
            var missingButtons = neededButtons - _menuButtons.Count;

            for (int i = 0; i < missingButtons; i++)
            {
                var newButton = Instantiate<MenuButton>(_buttonPrefab, _buttonContainer);
                _menuButtons.Add(newButton);
            }
        }

        #endregion 


        #region Private

        private List<MenuButton> _menuButtons = new List<MenuButton>();

        #endregion
    }
}