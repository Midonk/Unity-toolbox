using UnityEngine;
using UnityEditor;

namespace TF.MultiBuilder.Editor
{
    [CustomEditor(typeof(MultiBuildSettings))]
    public class MultiBuildSettingsEditor : UnityEditor.Editor 
    {
        public override void OnInspectorGUI() 
        {
            base.OnInspectorGUI();
            if(!GUILayout.Button("Build threads")) return;
            
            BuildManager.MultiBuildProcess();
        }
    }
}