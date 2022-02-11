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


        //Constructor for enums type
        public DebugMenuStateAttribute(string path, Type enumType, int defaultValue, bool quickMenu = false) : base(path, quickMenu)
        {
            EnumType = enumType;
            IntDefault = defaultValue;
        }
    }
}