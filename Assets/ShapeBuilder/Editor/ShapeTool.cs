using UnityEngine;
using UnityEditor;

public class ShapeTool<T> where T : Object
{
    #region Properties

    public T Target { get; set; }
    public ShapeBuilderSettings Settings => _settings;
         
    #endregion


    #region Main
    
    public void OnEnable()
    {
        _settings = ShapeBuilderSettings.GetOrCreate();
        _selection = new SelectionInfo(_settings.Builder);
        _drawer = new GeometryDrawer();
        _needsRepaint = true;
    }

    public void OnDisable()
    {

    }

    public void HandleEvent(Event guiEvent) 
    {
        switch (guiEvent.type)
        {
            case EventType.Repaint: 
                DrawShape();
                break;
            
            case EventType.Layout:
                PreventUnfocus();
                break;

            default: 
                HandleInput(guiEvent);
                break;
        }
    }

    private void DrawShape()
    {
        var builder = _settings.Builder;
        var shapes = builder.Shapes;
        for (int i = 0; i < builder.ShapeCount; i++)
        {
            var shape = shapes[i];
            _drawer.DrawSelectableShape(shape, _selection);
        }
    }

    private void HandleInput(Event guiEvent)
    {
        Ray mouseRay = HandleUtility.GUIPointToWorldRay(guiEvent.mousePosition);
        var mousePosition = mouseRay.origin * Vector2.one;
        UpdateSelection(mousePosition);

        var commands = _settings.InputTrigger.GetCommands(guiEvent);
        var builder = _settings.Builder;
        for (int i = 0; i < commands.Length; i++)
        {
            if(!(commands[i] is IShapeBuilderCommand shapeCommand)) continue;
            
            if(shapeCommand.Undoable)
            {
                Undo.RecordObject(Target, shapeCommand.ToString());
            }

            shapeCommand.Execute(_selection, mousePosition, builder);
            _needsRepaint = _needsRepaint || shapeCommand.NeedRepaint;
        }
    }
            
    #endregion
    

    #region Plumbery

    private void UpdateSelection(Vector2 mousePosition)
    {
        if (_settings.Builder.ShapeCount == 0) return;

        _selection.UpdateSelection(mousePosition);
        if(!_selection.Changed && !_needsRepaint) return;

        _needsRepaint = false;
        HandleUtility.Repaint();
    }

    private void PreventUnfocus()
    {
        var controlID = GUIUtility.GetControlID(FocusType.Passive);
        HandleUtility.AddDefaultControl(controlID);
    }
    
    #endregion


    #region Private Fields

    private IShapeSelectionInfo _selection;
    private ShapeBuilderSettings _settings;
    private GeometryDrawer _drawer;
    private bool _needsRepaint;

    #endregion
}