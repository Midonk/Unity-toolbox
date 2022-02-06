using UnityEngine;
using UnityEngine.UI;

namespace DebugMenu
{
    internal class MenuBoolButton : MenuButtonBase
    {
        [SerializeField] private Toggle _toggle;

        public override void Execute()
        {
            _toggle.isOn = !_toggle.isOn;
            DebugAttributeRegistry.InvokeMethod(_path, new object[]{_toggle.isOn});
        }
    }
}