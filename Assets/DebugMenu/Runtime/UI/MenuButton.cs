using System;
using UnityEngine;
using UnityEngine.UI;

// Container of multiple plausible displays that will show depending the method parameters

namespace DebugMenu
{
    [RequireComponent(typeof(Button))]
    public class MenuButton : MonoBehaviour
    {
        #region Exposed

        [SerializeField]
        private Text _text;
             
        #endregion


        #region Public Properties

        public string ReferenceName 
        {
            get => _referenceName; 
            set
            {
                _referenceName = value;
                name = _referenceName;
                _text.text = _referenceName;
            }
        }
             
        #endregion


        #region Main

        public void OnClick()
        {
            MenuRootPanel.Instance.ChangePanel(ReferenceName);
        }

        public void OnCancel()
        {
            MenuRootPanel.Instance.Return();
        }

        public void Prepare(string buttonName, Type buttonType)
        {
            ReferenceName = buttonName;
            var displayedUI = DefineDisplay(buttonType);
        }

        private DebugButtonDisplay DefineDisplay(Type buttonType) => nameof(buttonType) switch
        {
            "int" => null,
            "float" => null,
            "bool" => null,
            "string" => null,
            "enum" => null,
            _ => null
        };

        #endregion


        #region Private

        private string _referenceName;

        #endregion
    }
}