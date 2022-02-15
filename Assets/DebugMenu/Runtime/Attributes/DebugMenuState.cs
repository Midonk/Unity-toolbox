using System;

// Concrete attribute

namespace TF.DebugMenu.Attributes
{
    public class DebugMenuStateAttribute : DebugMenuAttribute
    {
        #region Public Properties

        public int IntDefault { get; private set; }
        public Type EnumType { get; private set; }

        #endregion 


        /// <summary>
        /// Use this attribute to reference a method that take an enum parameter to the Debug Menu and display it as an handle
        /// </summary>
        /// <param name="path">Ex: "my/custom/debug/path"</param>
        /// <param name="enumType">Type of the enum to use</param>
        /// <param name="defaultValue">Default value</param>
        /// <param name="quickMenu">Register this path to a quick access menu</param>
        public DebugMenuStateAttribute(string path, Type enumType, int defaultValue, bool quickMenu = false) : base(path, quickMenu)
        {
            EnumType = enumType;
            IntDefault = defaultValue;
        }
    }
}