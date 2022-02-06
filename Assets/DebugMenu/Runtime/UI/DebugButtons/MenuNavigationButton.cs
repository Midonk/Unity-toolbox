using UnityEngine;

namespace DebugMenu
{
    internal class MenuNavigationButton : MenuButtonBase
    {
        public override void Execute()
        {
            MenuRootPanel.Instance.ChangePanel(_path);
        }

        private void OnGUI() 
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