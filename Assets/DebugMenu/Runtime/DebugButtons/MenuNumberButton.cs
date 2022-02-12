using UnityEngine;
using TF.DebugMenu.Core;
using TF.DebugMenu.Components;
using TF.DebugMenu.Attributes;
using TF.DebugMenu.Utils;

namespace TF.DebugMenu.Buttons
{
    internal class MenuNumberButton : MenuButtonBase
    {
        #region Exposed

        [SerializeField] private NumberHandle _handle;
            
        #endregion


        #region Unity API

        private void OnGUI() 
        {
            var evt = Event.current;
            if(currentSelectionState != SelectionState.Selected) return;
            if(evt.type != EventType.KeyDown) return;
            if (evt.IsNumeric())
            {
                _handle.EnableManualInput();
                return;
            }

            if(evt.keyCode != KeyCode.LeftArrow && evt.keyCode != KeyCode.RightArrow) return;

            _handle.Select();
            evt.Use();
        }
            
        #endregion


        #region Main

        public override void Build(string path)
        {
            base.Build(path);
            var attribute = (DebugMenuIntAttribute)DebugAttributeRegistry.GetAttribute(path);
            _handle.CurrentValue = attribute.IntDefault;
        }

        public override void Execute()
        {
            DebugAttributeRegistry.InvokeMethod(_path, new object[]{_handle.CurrentValue});
        }

        #endregion
    }
}
