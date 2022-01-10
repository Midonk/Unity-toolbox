using UnityEngine;
using UnityEditor;

namespace Thomas.Test.New
{    
    [CustomEditor(typeof(ShapeBuilder))]
    public class ShapeEditor : Editor 
    {
        #region Unity API

        public override void OnInspectorGUI() 
        {
            DrawHelpBox();
            base.OnInspectorGUI();
        }

        private void OnSceneGUI() 
        {
            Event guiEvent = Event.current;
            switch (guiEvent.type)
            {
                case EventType.Repaint: 
                    Draw();
                    break;
                
                case EventType.Layout:
                    PreventUnfocus();
                    break;

                default: 
                    HandleInput(guiEvent);
                    break;
            }
        }

        private void OnEnable()
        {
            _builder = (ShapeBuilder)target;
            _inputTrigger = _builder.InputTrigger;
            _selection = new SelectionInfo(_builder); 
            _info = _inputTrigger.GetInputInfo();
            _needsRepaint = true;
            Tools.hidden = true;
        }

        private void OnDisable()
        {
            Tools.hidden = false;
        }
             
        #endregion        

        
        #region Main

        private void Draw()
        {
            for (int i = 0; i < _builder.Shapes.Length; i++)
            {
                var shape = _builder.Shapes[i];
                for (int j = 0; j < shape.Vertices.Length; j++)
                {
                    //vertex
                    var vertex = shape.Vertices[j];
                    var isCurrentShape = _selection.CurrentShape == shape;
                    var vertexIsHovered = _selection.HoveredVertex == vertex;
                    var vertexIsSelected = isCurrentShape && _selection.SelectedVertex == _selection.CurrentShape?.GetVertexIndex(vertex);
                    Handles.color = vertexIsSelected ? _selectedColor : 
                                    vertexIsHovered ? _hoveredColor : 
                                    !isCurrentShape ? _inactiveColor : _vertexColor;
                    Handles.DrawSolidDisc(vertex, Vector3.back, _builder.VertexRadius);
                    if(isCurrentShape)
                    {
                        Handles.Label(vertex + new Vector2(_builder.VertexRadius, -_builder.VertexRadius), j.ToString());
                    }

                    //edges
                    var nextVertex = shape.Vertices[(j + 1) % shape.Vertices.Length];
                    var lineIsHovered = _selection.HoveredEdge == vertex;
                    Handles.color = lineIsHovered ? _hoveredColor : 
                                    !isCurrentShape ? _inactiveColor : _lineColor;
                    Handles.DrawDottedLine(vertex, nextVertex, 4);
                }   
            }
        }

        private void HandleInput(Event guiEvent)
        {
            Ray mouseRay = HandleUtility.GUIPointToWorldRay(guiEvent.mousePosition);
            var mousePosition = mouseRay.origin * Vector2.one;
            UpdateSelection(mousePosition);

            var commands = _inputTrigger.GetCommands(guiEvent);
            for (int i = 0; i < commands.Length; i++)
            {
                if(!(commands[i] is IShapeBuilderCommand shapeCommand)) continue;
                
                //Debug.Log(shapeCommand.ToString());
                if(shapeCommand.Undoable)
                {
                    Undo.RecordObject(_builder, shapeCommand.ToString());
                }

                shapeCommand.Execute(_selection, mousePosition, _builder);
                _needsRepaint = _needsRepaint || shapeCommand.NeedRepaint;
            }
        }
             
        #endregion
        

        #region Plumbery

        private void DrawHelpBox()
        {
            EditorGUILayout.HelpBox(_info, MessageType.Info);
        }
             
        private void UpdateSelection(Vector2 mousePosition)
        {
            if (_builder.Shapes.Length == 0) return;

            _selection.UpdateSelection(mousePosition);
            if(!_selection.Changed && !_needsRepaint) return;

            _needsRepaint = false;
            HandleUtility.Repaint();
        }

        private void PreventUnfocus()
        {
            HandleUtility.AddDefaultControl(GUIUtility.GetControlID(FocusType.Passive));
        }
        
        #endregion


        #region Private Fields

        private ShapeBuilder _builder;
        private SelectionInfo _selection;
        private EditorInputTrigger _inputTrigger;
        private string _info;
        private bool _needsRepaint;

        private readonly Color _vertexColor = Color.white;
        private readonly Color _lineColor = Color.yellow;
        private readonly Color _hoveredColor = Color.red;
        private readonly Color _selectedColor = Color.cyan;
        private readonly Color _inactiveColor = Color.grey;
        
        #endregion
    }
}