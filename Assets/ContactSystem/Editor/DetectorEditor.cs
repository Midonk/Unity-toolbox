using System.Text;
using UnityEditor;

namespace ContactSystem.Editor
{
    [CustomEditor(typeof(DetectorBase), true)]
    [CanEditMultipleObjects]
    public class DetectorEditor : UnityEditor.Editor
    {
        private DetectorBase _script;

        protected void OnEnable() 
        {
            _script = (DetectorBase)target;
        }

        public override void OnInspectorGUI() 
        {
            base.OnInspectorGUI();
            serializedObject.Update();

            EditorGUILayout.Space(12);
            EditorGUILayout.HelpBox(GetInfoMessage(), MessageType.None);

            serializedObject.ApplyModifiedProperties();
        }

        private string GetInfoMessage()
        {
            var elements = _script.Elements;
            StringBuilder infoMessage = new StringBuilder();             
            infoMessage.AppendLine($"Detected elements: {elements.Length}");
            for (int i = 0; i < elements.Length; i++)
            {
                var elementName = elements[i].name;
                infoMessage.AppendLine($"- {elementName}");
            }

            return infoMessage.ToString();
        }
    }
}