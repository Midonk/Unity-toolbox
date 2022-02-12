using UnityEngine;
using TF.DebugMenu.Core;

namespace TF.DebugMenu.Buttons
{
    public class MenuNavigationButton : MenuButtonBase
    {
        #region Unity API

        protected virtual void OnGUI() 
        {
            var evt = Event.current;
            if(evt.type != EventType.KeyDown) return;
            if(currentSelectionState != SelectionState.Selected) return;
            if(evt.keyCode != KeyCode.RightArrow) return;

            Execute();
            Event.current.Use();
        }

        #endregion


        #region Main

        public override void Execute()
        {
            DebugMenuHandler.Instance.ChangePanel(_path);
        }

        #endregion
    }
}