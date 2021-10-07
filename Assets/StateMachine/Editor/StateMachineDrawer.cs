using UnityEngine;
using UnityEditor;
using System.Reflection;
using System.Collections.Generic;
using System;
using System.Linq;

namespace StateMachine
{
    [CustomPropertyDrawer(typeof(StateMachine), true)]
    public class StateMachineDrawer: PropertyDrawer 
    {
        /* public StateMachineDrawer() : base()
        {
            var availableStates = typeof(StateMachine).GetField("_availableStates", BindingFlags.NonPublic);
            var currentState = typeof(StateMachine).GetField("_currentState", BindingFlags.NonPublic);

            _availableStates = (Dictionary<Type, State>)availableStates.GetValue(null);
        } */

        public override void OnGUI(Rect position, SerializedProperty property, GUIContent label) 
        {
            GetInstance(property);

            EditorGUI.BeginProperty(position, label, property);
            var fieldRect = new Rect(position.x, position.y, position.width, EditorGUIUtility.singleLineHeight);
            EditorGUI.LabelField(fieldRect, $"{property.displayName}:");    
            DrawStates(position);            
            EditorGUI.EndProperty();
        }

        private void DrawStates(Rect position)
        {
            if(_availableStates is null) return;

            for(var i = 0; i < _availableStates.Count; i++)
            {
                var rect = new Rect(position.x, position.y + EditorGUIUtility.singleLineHeight * (i + 1), position.width, EditorGUIUtility.singleLineHeight);
                string stateName = _availableStates.Keys.ToArray()[i].Name;
                if(_instance.CurrentState != null && _instance.CurrentState.GetType().Name.Equals(stateName))
                {
                    var color = new Color(0, .75f, 1, 0.25f);
                    EditorGUI.DrawRect(rect, color);
                }

                EditorGUI.LabelField(rect, stateName);
            }
        }

        public override float GetPropertyHeight(SerializedProperty property, GUIContent label)
        {
            if(_availableStates != null)
            {
                return EditorGUIUtility.singleLineHeight * (_availableStates.Count + 1);
            }

            else
            {
                return EditorGUIUtility.singleLineHeight;
            }
        }

        private void GetInstance(SerializedProperty property)
        {
            if(_instance != null) return;

            var availableStatesField = typeof(StateMachine).GetField("_availableStates", BindingFlags.NonPublic | BindingFlags.Instance);
            _instance = (StateMachine)fieldInfo.GetValue(property.serializedObject.targetObject);
            _availableStates = (Dictionary<Type, State>)availableStatesField.GetValue(_instance);
        }

        private StateMachine _instance;
        private string _lastCurrentStateName;
        private Dictionary<Type, State> _availableStates;
    }
}