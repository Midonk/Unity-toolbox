using System;

// Concrete attribute

namespace TF.DebugMenu.Attributes
{
    public class DebugMenuSliderAttribute : DebugMenuAttribute
    {
        #region Public Properties

        public int IntDefault { get; private set; }
        public int Min { get; private set; }
        public int Max { get; private set; }
        public string Unit { get; private set; }

        #endregion 
        
        /// <summary>
        /// Use this attribute to reference a method that take an int parameter to the Debug Menu as a Slider
        /// </summary>
        /// <param name="path">Ex: "my/custom/debug/path"</param>
        /// <param name="defaultValue">Default value</param>
        /// <param name="minValue">Minimum slider value</param>
        /// <param name="maxValue">Maximum slider value</param>
        /// <param name="unit">String to use as a unit (ex: %, Kg, ...)</param>
        /// <param name="quickMenu">Register this path to a quick access menu</param>
        /// <returns></returns>
        public DebugMenuSliderAttribute(string path, int defaultValue, int minValue, int maxValue, string unit, bool quickMenu = false) : base(path, quickMenu)
        {
            IntDefault = defaultValue;
            Min = minValue;
            Max = maxValue;
            Unit = unit;
        }
    }
}