using System;

namespace DebugMenu
{
    [AttributeUsage(AttributeTargets.Method,Inherited = true,AllowMultiple = false)]
    public class DebugMenuAttribute : Attribute
    {
        #region Public Properties

        public string Path { get; private set; }
        public bool IsQuickMenu { get; private set; }

        #endregion 


        #region Main
        
        public DebugMenuAttribute(string path, bool quickMenu = false)
        {
            Path = path;
            IsQuickMenu = quickMenu;
        }

        #endregion Main
    }
}