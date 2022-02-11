using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using UnityEngine;

namespace DebugMenu
{
    public class DebugMenuSettings : ToolSettings<DebugMenuSettings>
    {
        public string[] KnownAttributes
        {
            get
            {
                if(_attributeNames.Count == 0)
                {
                    FetchAttributeNames();
                }

                return _attributeNames.ToArray();
            }
        }

        public int GetAttributeIndex(string name)
        {
            return _attributeNames.IndexOf(name);
        }

        [SerializeField] private List<string> _attributeNames;

        private void FetchAttributeNames()
        {
            _attributeNames.Clear();
            var attributeBaseType = typeof(DebugMenuAttribute);
            var assemblies = DebugAttributeRegistry.Assemblies;
            _attributeNames = assemblies.SelectMany(assembly => assembly.GetTypes()
                                                                        .Where(type => type.IsClass && 
                                                                                    attributeBaseType.IsAssignableFrom(type))
                                                                        .Select(type => type.Name))
                                                                        .ToList();
        }

        public void RefreshAttributeList()
        {
            FetchAttributeNames();
        }
    }
}