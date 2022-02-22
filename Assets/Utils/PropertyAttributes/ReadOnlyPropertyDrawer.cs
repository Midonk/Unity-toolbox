using UnityEngine;
using UnityEditor;

namespace TF.Utils
{
    [CustomPropertyDrawer(typeof(ReadOnlyAttribute))]
    public class ReadOnlyPropertyDrawer: PropertyDrawer 
    {
        public override void OnGUI(Rect position, SerializedProperty property, GUIContent label) 
        {
            var readOnly = (ReadOnlyAttribute)attribute;
            EditorGUI.BeginDisabledGroup(true);

            EditorGUI.PropertyField(position, property, label);

            EditorGUI.EndDisabledGroup();
        }
    }
}