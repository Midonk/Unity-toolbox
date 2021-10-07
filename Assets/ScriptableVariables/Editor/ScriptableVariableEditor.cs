using UnityEditor;

namespace ScriptableVaribles.Editor
{
    [CustomEditor(typeof(ScriptableVariable<>), true)]
    public class ScriptableVariableEditor : UnityEditor.Editor 
    {
        private SerializedProperty _defaultValue;
        private SerializedProperty _value;
        private SerializedProperty _resetValueOnExitPlayMode;

        private void OnEnable() 
        {
            _defaultValue = serializedObject.FindProperty("_defaultValue");
            _value = serializedObject.FindProperty("_value");
            _resetValueOnExitPlayMode = serializedObject.FindProperty("_resetValueOnExitPlayMode");
        }

        public override void OnInspectorGUI() 
        {
            serializedObject.Update();

            if(_resetValueOnExitPlayMode.boolValue)
            {
                EditorGUILayout.PropertyField(_defaultValue);
            }

            else
            {
                EditorGUILayout.PropertyField(_value);
            }
                
            EditorGUILayout.PropertyField(_resetValueOnExitPlayMode);

            serializedObject.ApplyModifiedProperties();
        }
    }
}