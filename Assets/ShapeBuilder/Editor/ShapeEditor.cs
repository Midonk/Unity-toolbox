using UnityEngine;
using UnityEditor;

//CLOSED
    
[CustomEditor(typeof(SampleMonobehaviour))]
public class ShapeEditor : Editor 
{
    #region Unity API

    public override void OnInspectorGUI() 
    {
        base.OnInspectorGUI();
        DrawHelpBox();
    }

    private void OnEnable()
    {
        _tool.Target = (SampleMonobehaviour)target;
        _tool.OnEnable();
        Tools.hidden = true;
    }

    private void OnDisable()
    {
        _tool.OnDisable();
        Tools.hidden = false;
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

    private ShapeTool<SampleMonobehaviour> _tool = new ShapeTool<SampleMonobehaviour>();
    
    #endregion
}