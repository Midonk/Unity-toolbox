using UnityEngine;
using UnityEditor;

    /* [CustomPropertyDrawer(typeof(EditorInputTrigger.InputTrigger))]
    public class InputTriggerDrawer : PropertyDrawer 
    {
        private int _elementIndex;
        private readonly float lineHeight = EditorGUIUtility.singleLineHeight + 2;


        public override void OnGUI(Rect position, SerializedProperty property, GUIContent label) 
        {
            var name = property.FindPropertyRelative("name");
            var eventType = property.FindPropertyRelative("eventType"); 
            var key = property.FindPropertyRelative("key");
            var modifiers = property.FindPropertyRelative("modifiers");
            var mouseButton = property.FindPropertyRelative("mouseButton");
            var commands = property.FindPropertyRelative("commands");

            _elementIndex = 0;
            var nameRect = new Rect(position.x, GetHeight(position, name), position.width, lineHeight);
            var eventTypeRect = new Rect(position.x, GetHeight(position, eventType), position.width, lineHeight);
            var keyRect = new Rect(position.x, GetHeight(position, key), position.width, lineHeight);
            var modifiersRect = new Rect(position.x, GetHeight(position, modifiers), position.width, lineHeight);
            var mouseButtonRect = new Rect(position.x, GetHeight(position, mouseButton), position.width, lineHeight);
            var commandsRect = new Rect(position.x, GetHeight(position, commands), position.width, lineHeight);

            EditorGUI.PropertyField(nameRect, name);
            EditorGUI.PropertyField(eventTypeRect, eventType);
            EditorGUI.PropertyField(keyRect, key);
            EditorGUI.PropertyField(modifiersRect, modifiers);
            EditorGUI.PropertyField(mouseButtonRect, mouseButton);
            EditorGUI.PropertyField(commandsRect, commands);
        }

        public override float GetPropertyHeight(SerializedProperty property, GUIContent label)
        {
            return _elementIndex * lineHeight;
        }

        private float GetHeight(Rect position, SerializedProperty property)
        {
            var height = position.y + lineHeight * _elementIndex;
            var isMultiline = property.isArray && property.isExpanded;
            _elementIndex += isMultiline ? property.arraySize + 1 : 1;
            return height;
        }
    } */