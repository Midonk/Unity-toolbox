using UnityEngine;

namespace DebugMenu
{
    internal class MenuEnumButton : MenuButtonBase
    {
        [SerializeField] private EnumHandle _handle;

        public override void Build(string path)
        {
            base.Build(path);
            var attribute = (DebugMenuStateAttribute)DebugAttributeRegistry.GetAttribute(path);
            _handle.SetType(attribute.EnumType);
            _handle.EnumValue = attribute.IntDefault;
        }

        public override void Execute()
        {
            DebugAttributeRegistry.InvokeMethod(_path, new object[]{_handle.EnumValue});
        }

        private void OnGUI() 
        {
            var evt = Event.current;
            if(currentSelectionState != SelectionState.Selected) return;
            if(evt.type != EventType.KeyDown) return;
            if(evt.keyCode != KeyCode.LeftArrow && evt.keyCode != KeyCode.RightArrow) return;

            _handle.Select();
            Event.current.Use();
        }
    }
}