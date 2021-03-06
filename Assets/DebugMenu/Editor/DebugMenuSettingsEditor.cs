using UnityEngine;
using UnityEditor;
using System.Text;

namespace TF.DebugMenu.Editor
{
    
    [CustomEditor(typeof(DebugMenuSettings))]
    public class DebugMenuSettingsEditor : UnityEditor.Editor 
    {
        #region Unity API

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

        #endregion

        
        #region Private Fields

        private StringBuilder _names = new StringBuilder();
        private DebugMenuSettings _settings;
            
        #endregion
    }
}