using UnityEngine;
using UnityEditor;
using TF.DebugMenu.Core;

namespace TF.DebugMenu.Editor
{
    [CustomPropertyDrawer(typeof(MenuButtonLink))]
    public class MenuButtonLinkDrawer : PropertyDrawer 
    {
        public override void OnGUI(Rect position, SerializedProperty property, GUIContent label) 
        {
            EditorGUI.BeginProperty(position, label, property);

            var buttonType = property.FindPropertyRelative("ButtonType");
            var buttonDisplay = property.FindPropertyRelative("ButtonDisplay");
            var settings = DebugMenuSettings.GetOrCreate();
            var attributeNames = settings.KnownAttributes;
            
            var selectedType = settings.GetAttributeIndex(buttonType.stringValue);
            if(selectedType == -1)
            {
                selectedType = 0;
            }

            var popupRect = new Rect(position.x, position.y, position.width / 2, position.height);
            var fieldRect = new Rect(position.x + popupRect.width, position.y, position.width / 2, position.height);

            selectedType = EditorGUI.Popup(popupRect, selectedType, attributeNames);
            EditorGUI.PropertyField(fieldRect, buttonDisplay, GUIContent.none);

            buttonType.stringValue = attributeNames[selectedType];

            EditorGUI.EndProperty();
        }

        public override float GetPropertyHeight(SerializedProperty property, GUIContent label)
        {
            return EditorGUIUtility.singleLineHeight;
        }
    }
}