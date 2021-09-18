using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;
using System;

namespace DebugMenu
{
    public class MenuPanel : MonoBehaviour
    {
        #region Exposed


        [SerializeField]
        private Text _headerTitle;

        [SerializeField]
        private MenuButton _buttonPrefab;

        public string ParentPath
        {
            get => _parent;
            set => _parent = value;
        }

        #endregion  


        #region Unity API

        private void OnEnable()
        {
            EventSystem.current.SetSelectedGameObject(null);
        }

        #endregion Unity API


        #region Main

        public void RebuildPanel(string[] buttons, string header)
        {
            CheckButtonPool(buttons.Length);
            PrepareButtons(buttons);
        }

        private void PrepareButtons(string[] buttons)
        {
            for(int i = 0; i < buttons.Length; i++)
            {
                _menuButtons[i].ReferenceName = buttons[i];
                _menuButtons[i].gameObject.SetActive(true);
            }

            for (int i = buttons.Length; i < _menuButtons.Count; i++)
            {
                _menuButtons[i].gameObject.SetActive(false);
            }
        }

        private void CheckButtonPool(int neededButtons)
        {
            if(_menuButtons.Count >= neededButtons) return;
            
            var missingButtons = neededButtons - _menuButtons.Count;

            for (int i = 0; i < missingButtons; i++)
            {
                var newButton = Instantiate<MenuButton>(_buttonPrefab);
                _menuButtons.Add(newButton);
            }
        }

        #endregion


        #region Utils

        public static void RegisterButton(MenuButton button)
        {
            if(_menuButtons.Contains(button)) return;

            _menuButtons.Add(button);
        }

        #endregion 


        #region Private

        private string _parent;
        private bool _isGenerated;
        private static List<MenuButton> _menuButtons = new List<MenuButton>();

        #endregion
    }
}