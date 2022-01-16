using UnityEngine;
using UnityEditor;
using System.Collections.Generic;

public class ShapeBuilderEditorWindow : EditorWindow 
{
    [SerializeField] private Injector _injector;


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
        _tool.OnEnable(this);
        _injector = new Injector(_shapes, UpdateShapes);
        SceneView.duringSceneGui += OnSceneGUI;
    }

    private void UpdateShapes()
    {
        var builderShapes = _tool.Settings.Builder.Shapes;
        _shapes = new List<Shape>();
        for (int i = 0; i < builderShapes.Length; i++)
        {
            _shapes.Add((Shape)builderShapes[i]);
        }
        
        _injector.SetSource(_shapes);
    }

    private void OnDisable() 
    {
        _tool.OnDisable();
        SceneView.duringSceneGui -= OnSceneGUI;
    }

    private void OnGUI() 
    {
        var serializedObject = new SerializedObject(this);
        var property = serializedObject.FindProperty("_injector");
        EditorGUILayout.PropertyField(property);
    }
         
    #endregion    
    
    private void OnSceneGUI(SceneView view)
    {
        var evt = Event.current;
        _tool.HandleEvent(evt);
    }

    #region Private Fields

    private ShapeTool _tool = new ShapeTool();
    private List<Shape> _shapes = new List<Shape>();

    #endregion
}