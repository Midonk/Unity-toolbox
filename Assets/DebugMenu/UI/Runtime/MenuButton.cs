using UnityEngine;
using UnityEngine.UI;

namespace DebugMenu
{
    [RequireComponent(typeof(Button))]
    public class MenuButton : MonoBehaviour
    {
        public string ReferenceName {
            get => _referenceName; 
            set
            {
                _referenceName = value;
                name = _referenceName;
            }
        }


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