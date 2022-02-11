using System;

namespace TF.DebugMenu.Attributes
{
    public class DebugMenuIntAttribute : DebugMenuAttribute
    {
        #region Public Properties

        public int IntDefault { get; private set; }

        #endregion 

        
        //Constructor for Number type
        public DebugMenuIntAttribute(string path, int defaultValue, bool quickMenu = false) : base(path, quickMenu)
        {
            IntDefault = defaultValue;
        }
    }
}