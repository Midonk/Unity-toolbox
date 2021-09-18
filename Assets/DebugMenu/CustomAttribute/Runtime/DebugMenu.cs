using System;

namespace DebugMenu.CustomAttribute.Runtime
{
    [AttributeUsage(AttributeTargets.Method,Inherited = true,AllowMultiple = false)]
    public class DebugMenuAttribute : Attribute
    {
        #region Public

        public string Path { get; set; } /* => _path; */

        public bool IsQuickMenu { get; set; }

        #endregion 


        #region Main

        public DebugMenuAttribute(string path)
        {
            Path = path;
        }
        
        public DebugMenuAttribute(string path, bool quickMenu)
        {
            Path = path;
            IsQuickMenu = quickMenu;
        }

        #endregion Main


        /* #region Private

        private string _path;

        #endregion Private */
    }
}