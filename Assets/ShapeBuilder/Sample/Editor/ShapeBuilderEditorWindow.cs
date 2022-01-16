using UnityEngine;
using UnityEditor;

public class ShapeBuilderEditorWindow : EditorWindow 
{
    #region Unity API

    [MenuItem("Tools/Shape builder")]
    private static void ShowWindow() 
    {
        var window = GetWindow<ShapeBuilderEditorWindow>();
        window.titleContent = new GUIContent("Shape builder");
        window.Show();
    }

    private void OnEnable() 
    {
        _tool.OnEnable();
        SceneView.duringSceneGui += OnSceneGUI;
    }

    private void OnDisable() 
    {
        _tool.OnDisable();
        SceneView.duringSceneGui -= OnSceneGUI;
    }

    private void OnGUI() 
    {
        var target = EditorGUILayout.ObjectField("Target", _target, typeof(SampleMonobehaviour), true);
        var button = GUILayout.Button("Bake shape");
        _target = (SampleMonobehaviour)target;
    }
         
    #endregion    
    
    private void OnSceneGUI(SceneView view)
    {
        var evt = Event.current;
        _tool.HandleEvent(evt);
    }

    #region Private Fields

    private ShapeTool<SampleMonobehaviour> _tool = new ShapeTool<SampleMonobehaviour>();
    private SampleMonobehaviour _target;
    
    #endregion
}