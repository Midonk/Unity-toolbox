using UnityEngine;
using UnityEditor;

[CustomEditor(typeof(RectTransform))]
public class RectTransformEditor : Editor {
    /* public override void OnInspectorGUI() {
        base.OnInspectorGUI();
        /* var mytarget = (RectTransform)target;
        if(GUILayout.Button("YOUPI"))
        {
            mytarget.AnchorToCorner();
        } 
    } */
}
