using UnityEngine;
using UnityEngine.UI;
using TF.DebugMenu.Core;
using TF.DebugMenu.Attributes;

namespace TF.DebugMenu.Buttons
{
    internal class MenuBoolButton : MenuButtonBase
    {
        #region Exposed

        [SerializeField] private Toggle _toggle;

        #endregion

        
        #region Main

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

        #endregion
    }
}