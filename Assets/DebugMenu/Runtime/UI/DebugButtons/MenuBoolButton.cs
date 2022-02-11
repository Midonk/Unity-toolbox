using UnityEngine;
using UnityEngine.UI;

namespace DebugMenu
{
    internal class MenuBoolButton : MenuButtonBase
    {
        [SerializeField] private Toggle _toggle;

        public override void Build(string path)
        {
            base.Build(path);
            var attribute = (DebugMenuToggleAttribute)DebugAttributeRegistry.GetAttribute(path);
            _toggle.isOn = attribute.BoolDefault;
        }

        public override void Execute()
        {
            _toggle.isOn = !_toggle.isOn;
            DebugAttributeRegistry.InvokeMethod(_path, new object[]{_toggle.isOn});
        }
    }
}