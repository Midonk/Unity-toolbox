using UnityEngine;
using UnityEditor;

public class ShapeBuilderEditorWindow : EditorWindow 
{
    [SerializeField] private ProvidedSourceInjector<Shape[]> _injector;


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
        _injector = new ProvidedSourceInjector<Shape[]>(_shapes);
        SceneView.duringSceneGui += OnSceneGUI;
    }

    /* private void UpdateShapes()
    {
        var builderShapes = _tool.Settings.Builder.Shapes;
        _shapes = new List<IShape>();
        for (int i = 0; i < builderShapes.Length; i++)
        {
            _shapes.Add((Shape)builderShapes[i]);
        }
        
        _injector.SetSource(_shapes);
    } */

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
    

    #region Main

    private void OnSceneGUI(SceneView view)
    {
        var evt = Event.current;
        _tool.HandleEvent(evt);
    }
         
    #endregion


    #region Private Fields

    private ShapeTool _tool = new ShapeTool();
    private Shape[] _shapes = new Shape[0];

    #endregion
}