using System.Linq;
using System.Reflection;
using System;

namespace DebugMenu
{
    [System.Serializable]
    public struct MenuButtonLink
    {
        public string ButtonType;
        public MenuButtonBase ButtonDisplay;
    }
}