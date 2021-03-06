using UnityEditor;

namespace ContactSystem.Editor
{
    [CustomEditor(typeof(Detector<>), true)]
    [CanEditMultipleObjects]
    public class DetectorEditor : ContactBaseEditor
    {
        private SerializedProperty _elements;

        protected override void OnEnable() 
        {
            base.OnEnable();
            _elements = serializedObject.FindProperty("_detectedElements");
        }

        public override void OnInspectorGUI() 
        {
            base.OnInspectorGUI();
            serializedObject.Update();
            
            EditorGUILayout.Space(12);
            var infoMessage = $"Detected elements: {_elements.arraySize}";
            EditorGUILayout.HelpBox(infoMessage, MessageType.None);

            serializedObject.ApplyModifiedProperties();
        }
    }
}