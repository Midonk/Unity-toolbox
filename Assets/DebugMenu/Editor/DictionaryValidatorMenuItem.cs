using UnityEditor;
using UnityEngine;
using TF.DebugMenu.Core;

using System.Text;

namespace TF.DebugMenu.Editor
{
    public class DictionaryValidatorMenuItem
    {
        #region Main

        /// <summary>
        /// Logs various informations relative to the Debug attribute references
        /// <summary>
        [MenuItem("Tools/Debug Menu/Log references")]
        public static void LogAttributeReferences()
        {
            DebugAttributeRegistry.RetreivePaths();
            if(DebugAttributeRegistry.Paths.Length == 0)
            {
                Debug.Log("No function found...");
                return;
            }

            var outputMethods = new StringBuilder();
            outputMethods.AppendLine($"Methods found (<color=orange>{DebugAttributeRegistry.Paths.Length}</color>):");
            outputMethods.AppendLine(LogAccumulator(DebugAttributeRegistry.Paths));

            var quickPaths = DebugAttributeRegistry.GetQuickPaths();
            var outputQuick = new StringBuilder();
            outputQuick.AppendLine($"Quick methods found (<color=orange>{quickPaths.Length}</color>)");

            if(quickPaths.Length > 0)
            {
                outputQuick.AppendLine(LogAccumulator(quickPaths));
            }

            Debug.Log(outputMethods);
            Debug.Log(outputQuick);
        }

        private static string LogAccumulator(string[] collection)
        {
            var accumulator = new StringBuilder();
            for (int i = 0; i < collection.Length; i++)
            {
                var path = DebugAttributeRegistry.Paths[i];
                var method = DebugAttributeRegistry.GetMethodName(path);
                accumulator.AppendLine($"Path: <color=cyan>{path}</color>\tMethod: <color=cyan>{method}</color>");
            }

            return accumulator.ToString();
        }

        #endregion
    }
}