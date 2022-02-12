using System.Collections.Generic;
using UnityEngine;
using TF.Utils;

// Process and provides required paths

namespace TF.DebugMenu.Core
{
    public class PathProcessor
    {
        #region Main

        public string[] FetchPaths(string comparingPath)
        {
            _methodPaths.Clear();
            var paths = DebugAttributeRegistry.Paths;
            var testedRoots = new List<string>();
            string separator = string.IsNullOrEmpty(comparingPath) ? "" : "/";
            foreach (var path in paths)
            {
                if(!path.StartsWith(comparingPath)) continue;
                
                //"truc/machin"
                var root = path.Remove(0, comparingPath.Length + separator.Length);
                var separatorIndex = root.IndexOf(SEPARATOR);
                if(separatorIndex > -1)
                {
                    root = root.Remove(separatorIndex);
                }

                if(testedRoots.Contains(root)) continue;

                testedRoots.Add(root);
                _methodPaths.Add($"{comparingPath}{separator}{root}");                
            }

            if(_methodPaths.Count == 0)
            {
                Debug.LogError($"'<color=red>{comparingPath}</color>' doesn't lead to any subfolder.");
            }

            return _methodPaths.ToArray();
        }
             
        #endregion


        #region Utils

        public string GetParentPath(string path)
        {
            return StringUtils.GetParentPath(path, SEPARATOR);
        }
            
        #endregion


        #region Private Fields

        private List<string> _methodPaths = new List<string>();
        private const char SEPARATOR = '/';

        #endregion
    }
}