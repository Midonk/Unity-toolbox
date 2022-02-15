using UnityEngine;
using UnityEditor;
using System.Text;
using System.IO;

namespace TF.MultiBuilder.Editor
{
    [CustomPropertyDrawer(typeof(BuildThread))]
    public class BuildThreadDrawer: PropertyDrawer 
    {
        public override void OnGUI(Rect position, SerializedProperty property, GUIContent label)
        {
            position.height = 0;
            var playerName = property.FindPropertyRelative("productName");
            var buildPath = property.FindPropertyRelative("buildPath");
            var buildTarget = property.FindPropertyRelative("buildTarget");
            var buildOptions = property.FindPropertyRelative("buildOptions");
            var scenes = property.FindPropertyRelative("scenesToBuild");

            var validThread = ValidateThread(playerName.stringValue, buildPath.stringValue, scenes.arraySize);
            EditorGUI.BeginProperty(position, label, property);
            var indent = EditorGUI.indentLevel;
            EditorGUI.indentLevel = 0;

            CreateLabel(property, label, playerName);
            position = DrawFoldout(position, property, label, validThread);
            if (property.isExpanded)
            {
                position = DrawProperties(position, property, playerName);
                position = DrawBuildPath(position, property, buildPath);
                position = DrawProperties(position, property, buildTarget, buildOptions);
                position = DrawScenesToBuild(position, property, scenes);
            }

            EditorGUI.EndDisabledGroup();
            EditorGUI.indentLevel = indent;
            EditorGUI.EndProperty();
        }

        private Rect DrawBuildPath(Rect position, SerializedProperty property, SerializedProperty buildPath)
        {
            var style = new GUIStyle(GUI.skin.button);
            var buttonLabel = new GUIContent("Browse");
            var buttonSize = style.CalcSize(buttonLabel);
            var buildPathString = buildPath.stringValue;
            var validFolder = Directory.Exists(buildPathString);
            var baseBuildPath = validFolder ? buildPathString : Application.dataPath;
            var buttonRect = new Rect(position.xMax - buttonSize.x, position.y + position.height, buttonSize.x, buttonSize.y);
            var fieldRect = new Rect(position.x, position.y + position.height, position.width - buttonSize.x, buttonSize.y);
            position.height += buttonSize.y + EditorGUIUtility.standardVerticalSpacing;
            
            if(GUI.Button(buttonRect, "Browse"))
            {
                var newPath = EditorUtility.OpenFolderPanel("Browse", baseBuildPath, "");
                if(!string.IsNullOrWhiteSpace(newPath))
                {
                    buildPath.stringValue = newPath;
                }
                
                property.serializedObject.ApplyModifiedProperties();
                GUIUtility.ExitGUI();
            }

            buildPath.stringValue = EditorGUI.TextField(fieldRect, buildPath.displayName, buildPath.stringValue);

            return position;
        }

        private static void CreateLabel(SerializedProperty property, GUIContent label, SerializedProperty playerName)
        {
            int index = int.Parse(property.displayName.Remove(0, 7));
            var newLabel = new StringBuilder($"Thread n° {index}");
            if (!string.IsNullOrWhiteSpace(playerName.stringValue))
            {
                newLabel.Append($" ({playerName.stringValue})");
            }

            label.text = newLabel.ToString();
        }

        private bool ValidateThread(string playerName, string buildPath, int sceneCount)
        {
            if(sceneCount == 0) return false;
            if(string.IsNullOrWhiteSpace(playerName)) return false;
            if(!Directory.Exists(buildPath)) return false;
            return true;
        }

        private Rect DrawProperties(Rect position, SerializedProperty property, params SerializedProperty[] properties)
        {
            for (int i = 0; i < properties.Length; i++)
            {
                var propertyRect = new Rect(position.x, position.y + position.height , position.width, EditorGUIUtility.singleLineHeight);
                EditorGUI.PropertyField(propertyRect, properties[i]);
                position.height  += EditorGUIUtility.singleLineHeight + EditorGUIUtility.standardVerticalSpacing;
            }

            return position;
        }

        private Rect DrawFoldout(Rect position, SerializedProperty property, GUIContent label, bool validThread)
        {
            var activeToggle = property.FindPropertyRelative("activeThread");
            var toggleWidth = EditorGUIUtility.singleLineHeight;
            var toggleRect = new Rect(position.xMax - toggleWidth, position.y, toggleWidth, toggleWidth);
            var foldoutRect = new Rect(position.x, position.y + position.height , position.width - toggleWidth, EditorGUIUtility.singleLineHeight);

            EditorGUI.PropertyField(toggleRect, activeToggle, GUIContent.none);
            var bufferColor = GUI.contentColor;
            if (!validThread && activeToggle.boolValue)
            {
                GUI.contentColor = new Color(1, 0.65f, 0);
            }

            EditorGUI.BeginDisabledGroup(!activeToggle.boolValue);
            property.isExpanded = EditorGUI.Foldout(foldoutRect, property.isExpanded, label);
            GUI.contentColor = bufferColor;
            position.height += EditorGUIUtility.singleLineHeight + EditorGUIUtility.standardVerticalSpacing;

            return position;
        }

        private Rect DrawScenesToBuild(Rect position, SerializedProperty property, SerializedProperty scenes)
        {
            var sceneHeight = scenes.isExpanded ? EditorGUI.GetPropertyHeight(scenes) : EditorGUIUtility.singleLineHeight;
            var scenesRect = new Rect(position.x, position.y + position.height , position.width, sceneHeight);
            position.height += sceneHeight;
            EditorGUI.PropertyField(scenesRect, scenes, true);

            return position;
        }

        public override float GetPropertyHeight(SerializedProperty property, GUIContent label)
        {
            /* if(!property.isExpanded) return EditorGUIUtility.singleLineHeight;

            var height = 0f;
            var enumerator = property.GetEnumerator();
            while (enumerator.MoveNext()) 
            {
                var prop = enumerator.Current as SerializedProperty;
                if (prop is null) continue;
                if(prop.isArray)
                {
                    height += EditorGUIUtility.singleLineHeight * (prop.CountInProperty() - 1);
                }

                else
                {
                    height += EditorGUI.GetPropertyHeight(prop);
                }
            } */

            return EditorGUI.GetPropertyHeight(property); //height;
        }
    }
}