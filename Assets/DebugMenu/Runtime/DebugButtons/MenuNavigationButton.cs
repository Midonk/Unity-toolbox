using UnityEngine;
using TF.DebugMenu.Core;

namespace TF.DebugMenu.Buttons
{
    public class MenuNavigationButton : MenuButtonBase
    {
        public override void Execute()
        {
            DebugMenuHandler.Instance.ChangePanel(_path);
        }

        protected virtual void OnGUI() 
        {
            var evt = Event.current;
            if(currentSelectionState != SelectionState.Selected) return;
            if(evt.type != EventType.KeyDown) return;
            if(evt.keyCode != KeyCode.LeftArrow && evt.keyCode != KeyCode.RightArrow) return;

            Execute();
            Event.current.Use();
        }
    }
}