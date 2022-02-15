using System;

// Concrete attribute

namespace TF.DebugMenu.Attributes
{
    public class DebugMenuToggleAttribute : DebugMenuAttribute
    {
        #region Public Properties

        public bool BoolDefault { get; private set; }

        #endregion
        
        /// <summary>
        /// Use this attribute to reference a method that take a bool parameter to the Debug Menu and display it as a toggle
        /// </summary>
        /// <param name="path">Ex: "my/custom/debug/path"</param>
        /// <param name="defaultValue">Default value</param>
        /// <param name="quickMenu">Register this path to a quick access menu</param>
        public DebugMenuToggleAttribute(string path, bool defaultValue, bool quickMenu = false) : base(path, quickMenu)
        {
            BoolDefault = defaultValue;
        }
    }
}