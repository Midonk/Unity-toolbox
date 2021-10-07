using UnityEngine;
using UnityEditor;

namespace ContactSystem.Editor
{
    [CustomEditor(typeof(ContactHandler))]
    [CanEditMultipleObjects]
    public class ContactHandlerEditor : ContactBaseEditor
    {
        SerializedProperty _onEnter;
        SerializedProperty _onExit;

        protected override void OnEnable() 
        {
            base.OnEnable();
            _onEnter = serializedObject.FindProperty("_onEnter");
            _onExit = serializedObject.FindProperty("_onExit");
        }

        public override void OnInspectorGUI()
        {
            base.OnInspectorGUI();
            serializedObject.Update();
            
            GUILayout.Space(12);
            EditorGUILayout.PropertyField(_onEnter, label:new GUIContent($"On Enter"));
            EditorGUILayout.PropertyField(_onExit, label:new GUIContent($"On Exit"));

            serializedObject.ApplyModifiedProperties();
        }
    }   
}