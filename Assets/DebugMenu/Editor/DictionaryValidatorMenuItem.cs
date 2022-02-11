using UnityEditor;
using System.Diagnostics;
using TF.DebugMenu.Core;

using Debug = UnityEngine.Debug;

namespace TF.DebugMenu.Editor
{
    public class DictionaryValidatorMenuItem
    {
        #region Main

        /// <summary>
        ///     Logs various informations relative to the Debug attribute reference
        /// <summary>
        [MenuItem("Debug Menu/Log references")]
        [Conditional("DEBUG")]
        public static void LogAttributeReferences()
        {
            DebugAttributeRegistry.RetreivePaths();
            string outputMethods = $"Methods found (<color=orange>{DebugAttributeRegistry.Paths.Length}</color>): ";
            if(DebugAttributeRegistry.Paths.Length == 0)
            {
                outputMethods = "No function found...";
                Debug.Log(outputMethods);
                return;
            }

            outputMethods += "\n";
            outputMethods += LogAccumulator(DebugAttributeRegistry.Paths);

            var quickPaths = DebugAttributeRegistry.GetQuickPaths();
            string outputQuick = $"Quick methods found (<color=orange>{quickPaths.Length}</color>)";
            outputQuick += "\n";

            if(quickPaths.Length > 0)
            {
                outputQuick += LogAccumulator(quickPaths);
            }

            Debug.Log(outputMethods);
            Debug.Log(outputQuick);
        }

        private static string LogAccumulator(string[] collection)
        {
            var accumulation = "";
            for (int i = 0; i < collection.Length; i++)
            {
                var path = DebugAttributeRegistry.Paths[i];
                accumulation += $"<color=cyan>{path}</color>";
                if(i == DebugAttributeRegistry.Paths.Length) return accumulation;
                
                accumulation += "\n";
            }

            return accumulation;
        }

        #endregion
    }
}