using System;
using UnityEngine;
using UnityEngine.UI;

namespace DebugMenu
{
    public abstract class DebugButtonDisplay
    {
        public string ReferenceName { get; set; }

        public virtual void SetupDisplay(string buttonName)
        {
            ReferenceName = buttonName;
        }

        public abstract void CleanUp();
    }

    public class BoolButtonDisplay : DebugButtonDisplay
    {
        [SerializeField] private Toggle _toggle;

        public override void SetupDisplay(string buttonName)
        {
            base.SetupDisplay(buttonName);
            _toggle.onValueChanged.AddListener(UpdateBoolValue);
        }

        public override void CleanUp()
        {
            _toggle.onValueChanged.RemoveListener(UpdateBoolValue);
        }

        private void UpdateBoolValue(bool value)
        {
            DebugAttributeRegistry.InvokeMethod(ReferenceName, new object[]{ value });
        }
    }
    
    public class FloatButtonDisplay : DebugButtonDisplay
    {
        [SerializeField] private Toggle _toggle;

        public override void SetupDisplay(string buttonName)
        {
            base.SetupDisplay(buttonName);
            _toggle.onValueChanged.AddListener(UpdateBoolValue);
        }

        public override void CleanUp()
        {
            _toggle.onValueChanged.RemoveListener(UpdateBoolValue);
        }

        private void UpdateBoolValue(bool value)
        {
            DebugAttributeRegistry.InvokeMethod(ReferenceName, new object[]{ value });
        }
    }
}