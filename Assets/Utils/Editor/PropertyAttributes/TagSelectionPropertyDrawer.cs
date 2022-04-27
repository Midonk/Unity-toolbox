using UnityEngine;
using UnityEditor;
using UnityEditorInternal;  
using System.Collections.Generic;

namespace TF.Utils
{
    [CustomPropertyDrawer(typeof(TagMask))]
    public class TagSelectionPropertyDrawer : PropertyDrawer 
    {
        public override void OnGUI(Rect position, SerializedProperty property, GUIContent label) 
        {
            var tagSelection = property.FindPropertyRelative("SelectedTags");
            var tags = InternalEditorUtility.tags;
            int bitMask = ListToBitMask(tagSelection, tags);
            bitMask = EditorGUI.MaskField(position, label, bitMask, tags);
            var tagList = MaskToList(bitMask, tags);
            tagSelection.ClearArray();
            for (int i = tagList.Length - 1; i >= 0; i--)
            {
                tagSelection.InsertArrayElementAtIndex(0);
                tagSelection.GetArrayElementAtIndex(0).stringValue = tagList[i];
            }
        }

        private string[] MaskToList(int mask, string[] tags)
        {
            var selectedTags = new List<string>();
            for (int i = 0; i < tags.Length; i++)
            {
                if((mask & (1 << i)) != (1 << i)) continue;
                
                selectedTags.Add(tags[i]);
            }

            return selectedTags.ToArray();
        }

        private int ListToBitMask(SerializedProperty property, string[] tags)
        {
            int mask = 0;
            var selectedTags = new List<string>(property.arraySize);
            for (int i = 0; i < property.arraySize; i++)
            {
                var value = property.GetArrayElementAtIndex(i).stringValue;
                selectedTags.Add(value);
            }

            for (int i = tags.Length - 1; i >= 0; i--)
            {
                mask = mask << 1;
                mask += selectedTags.Contains(tags[i]) ? 1 : 0;
            }

            return mask;
        }
    }
}