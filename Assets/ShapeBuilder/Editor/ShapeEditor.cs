using UnityEngine;
using UnityEditor;
using System;

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
            if (GUILayout.Button("Bake Navigation"))
            {
                //var navShapes = 
                //_nav.BakeNavigation();
                //_needsRepaint = true;
            }
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
            _needsRepaint = true;
            Tools.hidden = true;
            /* if (_nav != null && _nav.shapes.Count == 0)
                _nav.Init();*/
        }

        private void OnDisable()
        {
            Tools.hidden = false;
        }
             
        #endregion        


        private void OnGUI() {
            if(GUILayout.Button("truc")) return;
        }

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
                    var vertexIsSelected = _selection.SelectedVertex == _selection.CurrentShape?.GetVertexIndex(vertex);
                    Handles.color = vertexIsSelected ? _selectedColor : vertexIsHovered ? _hoveredColor : !isCurrentShape ? _inactiveColor : _vertexColor;
                    Handles.DrawSolidDisc(vertex, Vector3.back, _builder.VertexRadius);

                    //edges
                    var nextVertex = shape.Vertices[(j + 1) % shape.Vertices.Length];
                    var lineIsHovered = _selection.HoveredEdge == vertex;
                    Handles.color = lineIsHovered ? _hoveredColor : !isCurrentShape ? _inactiveColor : _lineColor;
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
                
                Debug.Log(shapeCommand.ToString());
                if(shapeCommand.Undoable)
                {
                    Undo.RecordObject(_builder, shapeCommand.ToString());
                }

                shapeCommand.Execute(_selection, mousePosition, _builder);
                _needsRepaint = _needsRepaint || shapeCommand.NeedRepaint;
            }

        }
        

        #region Plumbery

        private void DrawHelpBox()
        {
            var helpString = "Shift + Click to start a new shape \n" +
                             "Click to add a point\n" +
                             "Tab to cycle through shapes\n" +
                             "Del to delete current shape";

            EditorGUILayout.HelpBox(helpString, MessageType.Info);
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
        private bool _needsRepaint;

        private readonly Color _vertexColor = Color.white;
        private readonly Color _lineColor = Color.yellow;
        private readonly Color _hoveredColor = Color.red;
        private readonly Color _selectedColor = Color.cyan;
        private readonly Color _inactiveColor = Color.grey;
        
        #endregion
    }
}