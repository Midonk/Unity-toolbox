using System;

// Concrete base attribute

namespace TF.DebugMenu.Attributes
{
    [AttributeUsage(AttributeTargets.Method, Inherited = true, AllowMultiple = false)]
    public class DebugMenuAttribute : System.Attribute
    {
        #region Public Properties

        public string Path { get; private set; }
        public bool IsQuickMenu { get; private set; }

        #endregion 

        //Base Debug attribute
        /// <summary>
        /// Use this attribute to reference a method that take no parameter to the Debug Menu
        /// </summary>
        /// <param name="path">Ex: "my/custom/debug/path"</param>
        /// <param name="quickMenu">Register this path to a quick access menu</param>
        public DebugMenuAttribute(string path, bool quickMenu = false)
        {
            Path = path;
            IsQuickMenu = quickMenu;
        }
    }
}