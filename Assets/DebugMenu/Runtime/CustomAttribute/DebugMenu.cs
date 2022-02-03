using System;

// Concrete attribute

namespace DebugMenu
{
    [AttributeUsage(AttributeTargets.Method, Inherited = true, AllowMultiple = false)]
    public class DebugMenuAttribute : System.Attribute
    {
        #region Public Properties

        public string Path { get; private set; }
        public bool IsQuickMenu { get; private set; }
        public bool BoolDefault { get; private set; }
        public int IntDefault { get; private set; }
        public Type EnumType { get; private set; }
        public int Min { get; private set; }
        public int Max { get; private set; }
        public string Unit { get; private set; }

        #endregion 

        
        //Constructor for void type
        /// <summary>
        /// 
        /// </summary>
        /// <param name="path">Ex: "my/custom/debug/path"</param>
        /// <param name="quickMenu">Register this path to a quick access menu</param>
        public DebugMenuAttribute(string path, bool quickMenu = false)
        {
            Path = path;
            IsQuickMenu = quickMenu;
        }
        
        //Constructor for slider type
        public DebugMenuAttribute(string path, int defaultValue, int minValue, int maxValue, string unit, bool quickMenu = false) : this(path, quickMenu)
        {
            IntDefault = defaultValue;
            Min = minValue;
            Max = maxValue;
        }
        
        //Constructor for Number type
        public DebugMenuAttribute(string path, int defaultValue, bool quickMenu = false) : this(path, quickMenu)
        {
            IntDefault = defaultValue;
        }
        
        //Constructor for enums type
        public DebugMenuAttribute(string path, Type enumType, int defaultValue, bool quickMenu = false) : this(path, quickMenu)
        {
            EnumType = enumType;
            IntDefault = defaultValue;
        }
    }
}