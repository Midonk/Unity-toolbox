using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using TF.Tool;
using TF.DebugMenu.Attributes;
using TF.DebugMenu.Core;

namespace TF.DebugMenu.Editor
{
    public class DebugMenuSettings : ToolSettings<DebugMenuSettings>
    {
        #region Properties

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

        #endregion

        
        #region Main

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

        #endregion

        
        #region Utils

        /// <summary>
        /// Retreive the attribute index by searching its name in the references
        /// </summary>
        /// <param name="name">Attribute type name</param>
        /// <returns></returns>
        public int GetAttributeIndex(string name)
        {
            return _attributeNames.IndexOf(name);
        }

        /// <summary>
        /// Refresh the attribute reference list
        /// </summary>
        public void RefreshAttributeList()
        {
            FetchAttributeNames();
        }
            
        #endregion


        #region Private Fields

        [SerializeField] private List<string> _attributeNames = new List<string>();
            
        #endregion
    }
}