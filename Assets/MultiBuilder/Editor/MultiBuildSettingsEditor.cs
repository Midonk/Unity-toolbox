using UnityEngine;
using UnityEditor;

namespace MultiBuild
{
    
    [CustomEditor(typeof(MultiBuildSettings))]
    public class MultiBuildSettingsEditor : Editor 
    {
        public override void OnInspectorGUI() 
        {
            base.OnInspectorGUI();
            if(GUILayout.Button("Build threads"))
            {
                MultiBuild.BuildManager.MultiBuildProcess();
            }    
        }
    }
}