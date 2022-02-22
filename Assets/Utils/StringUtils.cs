using UnityEngine;

namespace TF.Utils
{
    public static class StringUtils
    {
        public static string GetFirstElementOfPath(string path, params char[] separators)
        {
            for (int i = 0; i < separators.Length; i++)
            {
                var separator = separators[i];
                if(!path.Contains(separator.ToString())) continue;

                var separatorIndex = path.IndexOf(separator);
                if(separatorIndex == -1) return string.Empty;

                return path.Remove(separatorIndex);
            }

            throw new System.ArgumentException("None of the given <color=red>separators</color> found in the path <color=red>{path}</color>");
        }

        public static string GetLastElementOfPath(string path, params char[] separators)
        {
            var lastSeparatorIndex = path.LastIndexOfAny(separators);
            return path.Remove(0, lastSeparatorIndex);
        }
        
        public static string GetParentPath(string path, params char[] separators)
        {
            var lastSeparatorIndex = path.LastIndexOfAny(separators);
            if(lastSeparatorIndex == -1) return string.Empty;

            return path.Remove(lastSeparatorIndex);
        }
    }
}
