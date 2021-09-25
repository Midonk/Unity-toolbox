using UnityEngine;
using UnityEditor;

namespace ContactSystem
{
    [CustomEditor(typeof(ContactBase), true)]
    public class ContactBaseEditor : Editor 
    {
        private SerializedProperty _filterType;
        private SerializedProperty _contactTags;
        private SerializedProperty _contactLayer;

        protected virtual void OnEnable() 
        {
            _filterType = serializedObject.FindProperty("_filterType");
            _contactTags = serializedObject.FindProperty("_contactTags");
            _contactLayer = serializedObject.FindProperty("_contactLayer");
        }

        public override void OnInspectorGUI() 
        {
            serializedObject.Update();

            EditorGUILayout.PropertyField(_filterType);
            GUILayout.Space(12);

            if (_filterType.enumValueIndex == 2)
            {
                EditorGUILayout.PropertyField(_contactLayer);
            }

            else if (_filterType.enumValueIndex == 1)
            {
                EditorGUILayout.PropertyField(_contactTags);
            }
            
            else if (_filterType.enumValueIndex == 0)
            {
                EditorGUILayout.PropertyField(_contactLayer);
                EditorGUILayout.PropertyField(_contactTags);
            }  

            serializedObject.ApplyModifiedProperties();
        }
    }
}