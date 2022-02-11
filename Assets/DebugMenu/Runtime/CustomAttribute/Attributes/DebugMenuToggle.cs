using System;

// Concrete attribute

namespace DebugMenu
{
    public class DebugMenuToggleAttribute : DebugMenuAttribute
    {
        #region Public Properties

        public bool BoolDefault { get; private set; }

        #endregion
        
        public DebugMenuToggleAttribute(string path, bool defaultValue, bool quickMenu = false) : base(path, quickMenu)
        {
            BoolDefault = defaultValue;
        }
    }
}