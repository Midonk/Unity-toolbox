using System;

namespace TF.DebugMenu.Attributes
{
    public class DebugMenuIntAttribute : DebugMenuAttribute
    {
        #region Public Properties

        public int IntDefault { get; private set; }

        #endregion 

        
        /// <summary>
        /// Use this attribute to reference a method that take an int parameter to the Debug Menu and display it as an handle
        /// </summary>
        /// <param name="path">Ex: "my/custom/debug/path"</param>
        /// <param name="defaultValue">Default value</param>
        /// <param name="quickMenu">Register this path to a quick access menu</param>
        public DebugMenuIntAttribute(string path, int defaultValue, bool quickMenu = false) : base(path, quickMenu)
        {
            IntDefault = defaultValue;
        }
    }
}