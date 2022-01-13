using UnityEditor;
using UnityEngine;

namespace Thomas.Test.New
{
    public class GeometryDrawer
    {
        public void DrawSelectableShape(IShape shape, IShapeSelectionInfo selection)
        {
            for (int i = 0; i < shape.Vertices.Count; i++)
            {
                var vertex = shape.Vertices[i];
                var isCurrentShape = selection.CurrentShape == shape;
                DrawSelectableVertex(vertex, isCurrentShape, selection);

                var nextVertex = shape.Vertices[(i + 1) % shape.Vertices.Count];
                DrawSelectableEdge(vertex, nextVertex, isCurrentShape, selection);
                
                if (!isCurrentShape) continue;
                
                var labelOffset = new Vector2(selection.VertexRadius, -selection.VertexRadius);
                Handles.Label(vertex + labelOffset, i.ToString());
            }
        }

        private void DrawSelectableVertex(Vector2 vertex, bool isCurrentShape, IShapeSelectionInfo selection)
        {
            var vertexIsHovered = selection.HoveredVertex == vertex;
            var vertexIsSelected = isCurrentShape && selection.SelectedVertex == selection.CurrentShape?.GetVertexIndex(vertex);
            Handles.color = vertexIsSelected ? _selectedColor :
                            vertexIsHovered ? _hoveredColor :
                            !isCurrentShape ? _inactiveColor : _vertexColor;
            Handles.DrawSolidDisc(vertex, Vector3.back, selection.VertexRadius);
        }

        private void DrawSelectableEdge(Vector2 vertex, Vector2 nextVertex, bool isCurrentShape, IShapeSelectionInfo selection)
        {
            var lineIsHovered = selection.HoveredEdge == vertex;
            Handles.color = lineIsHovered ? _hoveredColor :
                            !isCurrentShape ? _inactiveColor : _lineColor;
            Handles.DrawDottedLine(vertex, nextVertex, 4);
        }
        
        private readonly Color _vertexColor = Color.white;
        private readonly Color _lineColor = Color.yellow;
        private readonly Color _hoveredColor = Color.red;
        private readonly Color _selectedColor = Color.cyan;
        private readonly Color _inactiveColor = Color.grey;
    }
}