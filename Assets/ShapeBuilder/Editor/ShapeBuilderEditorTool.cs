using UnityEngine;
using UnityEditor.EditorTools;
using UnityEditor;

[EditorTool("Shape builder", typeof(Camera))]
public class ShapeBuilderEditorTool : EditorTool
{
    [SerializeField] private Texture2D _icon;

    public override GUIContent toolbarIcon
    {
        get { return new GUIContent("Shape builder"); }
    }

    public override void OnToolGUI(EditorWindow window) 
    {
        DrawGUI(window);
    }

    private void OnSceneGUI(SceneView view) 
    {
        var evt = Event.current;
        _tool.HandleEvent(evt);
    }

    public void OnEnable()
    {
        _tool.OnEnable();
        //ToolManager. .activeToolChanged += ActiveToolDidChange;
        _toolIcon = new GUIContent("Shape builder", _icon, "Build shape...");
        SceneView.duringSceneGui += OnSceneGUI;
        Tools.hidden = true;
    }

    public void OnDisable()
    {
        _tool.OnDisable();
        SceneView.duringSceneGui -= OnSceneGUI;
        Tools.hidden = false;
    }

    private void DrawGUI(EditorWindow window)
    {
        var infos = _tool.Settings.InputTrigger.GetInputInfo();
        var content = new GUIContent(infos);
        var height = GUIStyle.none.CalcHeight(content, window.position.width - 10);
        var rect = new Rect(5, 5, window.position.width - 10, height + 5);
        EditorGUI.HelpBox(rect, infos, MessageType.Info);
    }

    private GUIContent _toolIcon;
    private ShapeTool<Camera> _tool = new ShapeTool<Camera>();
}