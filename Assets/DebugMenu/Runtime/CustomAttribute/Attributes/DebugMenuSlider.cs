using System;

// Concrete attribute

namespace DebugMenu
{
    public class DebugMenuSliderAttribute : DebugMenuAttribute
    {
        #region Public Properties

        public int IntDefault { get; private set; }
        public int Min { get; private set; }
        public int Max { get; private set; }
        public string Unit { get; private set; }

        #endregion 
        
        //Constructor for slider type
        public DebugMenuSliderAttribute(string path, int defaultValue, int minValue, int maxValue, string unit, bool quickMenu = false) : base(path, quickMenu)
        {
            IntDefault = defaultValue;
            Min = minValue;
            Max = maxValue;
            Unit = unit;
        }
    }
}