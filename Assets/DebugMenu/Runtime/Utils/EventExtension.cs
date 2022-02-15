using UnityEngine;

namespace TF.DebugMenu.Utils
{
    public static class EventExtension
    {
        public static bool IsNumeric(this Event evt) => evt.keyCode switch
        {
            KeyCode.Alpha0 => true,
            KeyCode.Alpha1 => true,
            KeyCode.Alpha2 => true,
            KeyCode.Alpha3 => true,
            KeyCode.Alpha4 => true,
            KeyCode.Alpha5 => true,
            KeyCode.Alpha6 => true,
            KeyCode.Alpha7 => true,
            KeyCode.Alpha8 => true,
            KeyCode.Alpha9 => true,
            KeyCode.Keypad0 => true,
            KeyCode.Keypad1 => true,
            KeyCode.Keypad2 => true,
            KeyCode.Keypad3 => true,
            KeyCode.Keypad4 => true,
            KeyCode.Keypad5 => true,
            KeyCode.Keypad6 => true,
            KeyCode.Keypad7 => true,
            KeyCode.Keypad8 => true,
            KeyCode.Keypad9 => true,
            _ => false
        };
        
        public static int GetNumber(this Event evt) => evt.keyCode switch
        {
            KeyCode.Alpha0 => 0,
            KeyCode.Alpha1 => 1,
            KeyCode.Alpha2 => 2,
            KeyCode.Alpha3 => 3,
            KeyCode.Alpha4 => 4,
            KeyCode.Alpha5 => 5,
            KeyCode.Alpha6 => 6,
            KeyCode.Alpha7 => 7,
            KeyCode.Alpha8 => 8,
            KeyCode.Alpha9 => 9,
            KeyCode.Keypad0 => 0,
            KeyCode.Keypad1 => 1,
            KeyCode.Keypad2 => 2,
            KeyCode.Keypad3 => 3,
            KeyCode.Keypad4 => 4,
            KeyCode.Keypad5 => 5,
            KeyCode.Keypad6 => 6,
            KeyCode.Keypad7 => 7,
            KeyCode.Keypad8 => 8,
            KeyCode.Keypad9 => 9,
            _ => -1
        };
    }
}