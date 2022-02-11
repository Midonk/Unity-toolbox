using UnityEngine;

namespace DebugMenu
{
    internal class MenuNumberButton : MenuButtonBase
    {
        [SerializeField] private NumberHandle _handle;
        public override void Build(string path)
        {
            base.Build(path);
            var attribute = (DebugMenuIntAttribute)DebugAttributeRegistry.GetAttribute(path);
            _handle.Number = attribute.IntDefault;
        }

        public override void Execute()
        {
            DebugAttributeRegistry.InvokeMethod(_path, new object[]{_handle.Number});
        }

        private void OnGUI() 
        {
            var evt = Event.current;
            if(currentSelectionState != SelectionState.Selected) return;
            if(evt.type != EventType.KeyDown) return;
            if(evt.numeric) Debug.Log("Foo");
            if(evt.keyCode != KeyCode.LeftArrow && evt.keyCode != KeyCode.RightArrow) return;

            _handle.Select();
            Event.current.Use();
        }
    }
}