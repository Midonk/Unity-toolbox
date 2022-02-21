using UnityEngine;
using UnityEngine.UI;
using TF.DebugMenu.Core;
using TF.DebugMenu.Attributes;

namespace TF.DebugMenu.Buttons
{
    internal class MenuSliderButton : MenuButtonBase
    {
        [SerializeField] private Slider _slider;
        [SerializeField] private Text _valueDisplay;

        public override void Build(string path)
        {
            base.Build(path);
            var attribute = (DebugMenuSliderAttribute)DebugAttributeRegistry.GetAttribute(path);
            _slider.minValue = attribute.Min;
            _slider.maxValue = attribute.Max;
            _slider.value = attribute.floatDefault;
        }

        public void UpdateValueDisplay(float value)
        {
            _valueDisplay.text = $"{value : #0.##}";
        }

        public override void Execute()
        {
            DebugAttributeRegistry.InvokeMethod(_path, new object[]{ _slider.value });
        }
    }
}