using UnityEditor;
using UnityEngine;

//CLOSED
    
[CustomEditor(typeof(Camera))]
public class ShapeCustomEditor : Editor 
{
    #region Unity API

    public override void OnInspectorGUI() 
    {
        base.OnInspectorGUI();
        DrawHelpBox();
    }

    private void OnEnable()
    {
        _tool.OnEnable((Camera)target);
    }

    private void OnDisable()
    {
        _tool.OnDisable();
    }
            
    #endregion        
    

    #region Main

    private void DrawHelpBox()
    {
        var infos = _tool.Settings?.InputTrigger.GetInputInfo();
        EditorGUILayout.HelpBox(infos, MessageType.Info);
    }
    
    #endregion


    #region Private Fields

    private ShapeTool _tool = new ShapeTool();
    
    #endregion
}