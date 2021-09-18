using UnityEngine;
using UnityEngine.UI;

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

        public string ReferenceName {
            get => _referenceName; 
            set
            {
                _referenceName = value;
                name = _referenceName;
            }
        }

        public string Text
        {
            get => _text.text;
            set => _text.text = value;
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

        #endregion


        #region Private

        private string _referenceName;
             
        #endregion
    }
}