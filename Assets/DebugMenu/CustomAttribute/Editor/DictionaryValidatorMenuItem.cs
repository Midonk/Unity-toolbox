using UnityEditor;
using System.Diagnostics;
using DebugMenu.CustomAttribute.Runtime;

namespace DebugMenu.CustomAttribute.Editor
{
    public class DictionaryValidatorMenuItem
    {
        #region Main

        [MenuItem("Debug Menu/Validate Methods")]
        [Conditional("DEBUG")]
        public static void TryValidate()
        {
            DebugAttributeRegistry.ValidateMethods();
            string output = $"Methods found (<color=orange>{DebugAttributeRegistry.Paths.Length}</color>): ";
            if(DebugAttributeRegistry.Paths.Length == 0)
            {
                output = "No function found...";
                UnityEngine.Debug.Log(output);
                return;
            }

            output += LogAccumulator(DebugAttributeRegistry.Paths);

            var quickPaths = DebugAttributeRegistry.GetQuickPaths();
            output += "\n";
            output += $"Quick methods found (<color=orange>{quickPaths.Length}</color>)";

            if(quickPaths.Length > 0)
            {
                output += ": ";
                output += LogAccumulator(quickPaths);
            }

            UnityEngine.Debug.Log(output);
        }

        private static string LogAccumulator(string[] collection)
        {
            var accumulation = "";
            for (int i = 0; i < collection.Length; i++)
            {
                var path = DebugAttributeRegistry.Paths[i];
                accumulation += $"<color=cyan>{path}</color>";
                if(i == DebugAttributeRegistry.Paths.Length) return accumulation;
                
                accumulation += ", ";
            }

            return accumulation;
        }

        #endregion
    }
}