using UnityEngine;
using UnityEditor;

using FilterType = ContactSystem.LayerTagFilter.FilterType;

namespace ContactSystem.Editor
{
    
    [CustomPropertyDrawer(typeof(LayerTagFilter))]
    public class LayerTagFilterDrawer: PropertyDrawer 
    {
        public override void OnGUI(Rect position, SerializedProperty property, GUIContent label) 
        {
            var filterTypeProperty = property.FindPropertyRelative("Type");
            var filterType = (FilterType)filterTypeProperty.enumValueIndex;
            var typeRect = new Rect(position.x, position.y, position.width, EditorGUIUtility.singleLineHeight);
            var verticalOffset = verticalSpacing;

            EditorGUI.PropertyField(typeRect, filterTypeProperty);

            if(filterType == FilterType.None) return;
            
            if(filterType == FilterType.Tag || filterType == FilterType.Both)
            {
                var tagProperty = property.FindPropertyRelative("Tags");
                var tagRect = new Rect(position.x, position.y + verticalOffset, position.width, EditorGUIUtility.singleLineHeight);
                EditorGUI.PropertyField(tagRect, tagProperty);
                verticalOffset += verticalSpacing;
            }
            
            if(filterType == FilterType.Layer || filterType == FilterType.Both)
            {
                var layerProperty = property.FindPropertyRelative("Layers");
                var layerRect = new Rect(position.x, position.y + verticalOffset, position.width, EditorGUIUtility.singleLineHeight);
                EditorGUI.PropertyField(layerRect, layerProperty);
            }
        }

        public override float GetPropertyHeight(SerializedProperty property, GUIContent label)
        {
            var height = base.GetPropertyHeight(property, label);
            var filterProperty = property.FindPropertyRelative("Type");
            var filter = (FilterType)filterProperty.enumValueIndex;
            height += filter switch
            {
                FilterType.None  => 0,
                FilterType.Tag   => verticalSpacing,
                FilterType.Layer => verticalSpacing,
                FilterType.Both  => verticalSpacing * 2,
                _                => throw new System.NotImplementedException()
            };

            return height;
        }

        private readonly float verticalSpacing = EditorGUIUtility.singleLineHeight + EditorGUIUtility.standardVerticalSpacing;
    }
}
