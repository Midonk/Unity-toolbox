using UnityEngine;
using UnityEditor;
using System.Text;

namespace DebugMenu
{
    
    [CustomEditor(typeof(DebugMenuSettings))]
    public class DebugMenuSettingsEditor : UnityEditor.Editor 
    {
        private void OnEnable() 
        {
            _names.Clear();
            _settings = DebugMenuSettings.GetOrCreate();
            var knownAttributes = _settings.KnownAttributes;
            for (int i = 0; i < knownAttributes.Length; i++)
            {
                _names.AppendLine(knownAttributes[i]);
            }
        }

        public override void OnInspectorGUI() 
        {
            EditorGUI.BeginDisabledGroup(true);
            base.OnInspectorGUI();
            EditorGUI.EndDisabledGroup();


            if(!GUILayout.Button("Refresh")) return;

            _settings.RefreshAttributeList(); 

        }

        private StringBuilder _names = new StringBuilder();
        private DebugMenuSettings _settings;
    }
}