using UnityEngine;
using TF.DebugMenu.Core;
using TF.DebugMenu.Attributes;
using TF.DebugMenu.Components;

namespace TF.DebugMenu.Buttons
{
    internal class MenuEnumButton : MenuButtonBase
    {
        #region Exposed

        [SerializeField] private EnumHandle _handle;
            
        #endregion
        
        
        #region Unity API

        private void OnGUI() 
        {
            var evt = Event.current;
            if(currentSelectionState != SelectionState.Selected) return;
            if(evt.type != EventType.KeyDown) return;
            if(evt.keyCode != KeyCode.LeftArrow && evt.keyCode != KeyCode.RightArrow) return;

            _handle.Select();
            Event.current.Use();
        }

        #endregion
        

        #region Main

        public override void Build(string path)
        {
            base.Build(path);
            var attribute = (DebugMenuStateAttribute)DebugAttributeRegistry.GetAttribute(path);
            _handle.SetType(attribute.EnumType);
            _handle.CurrentValue = attribute.IntDefault;
        }

        public override void Execute()
        {
            DebugAttributeRegistry.InvokeMethod(_path, new object[]{_handle.CurrentValue});
        }

        #endregion
    }
}