using System;
using UnityEngine;
using UnityEditor;

namespace Thomas.Test.New
{
    public class SelectionInfo : ISelectionInfo<ShapeBuilder>
    {
        public SelectionInfo(ShapeBuilder context)
        {
            Context = context;
        }

        public bool MouseHoveringVertex => HoveredVertex != null;
        public bool MouseHoveringEdge => HoveredEdge != null;
        public Vector2? HoveredVertex { get; private set; }
        public Vector2? HoveredEdge { get; private set; }// = vertex that makes the edge with vertex + 1
        public Shape CurrentShape { get; private set; }
        public int SelectedVertex { get; private set; } = -1;

        public ShapeBuilder Context { get; private set; }
        public bool Changed { get; private set; }

        public void UpdateSelection(Vector2 mousePosition)
        {
            for (int i = 0; i < Context.Shapes.Length; i++)
            {
                var vertices = Context.Shapes[i].Vertices;
            
                //vertex selection
                for (int j = 0; j < vertices.Length; j++)
                {
                    var vertex = vertices[j];
                    var isVertexSelected = CheckVertexSelection(vertex, mousePosition);
                    if(isVertexSelected) return;
                }

                //edge selection
                for (int j = 0; j < vertices.Length; j++)
                {
                    var vertex = vertices[j];
                    var nextVertex = vertices[(j + 1) % vertices.Length];
                    var isEdgeSelected = CheckEdgeSelection(vertex, nextVertex, mousePosition);
                    if(isEdgeSelected) return;
                }
                
                //nothing selected
                Changed = HoveredVertex != null || HoveredEdge != null;
                HoveredVertex = null;
                HoveredEdge = null;
            }
        }

        private bool CheckVertexSelection(Vector2 vertex, Vector2 mousePosition)
        {
            var mouseToVertex = vertex - mousePosition;
            if(mouseToVertex.sqrMagnitude > Context.VertexRadius * Context.VertexRadius) return false;
            
            Changed = vertex != HoveredVertex;
            if(!Changed) return true;

            HoveredVertex = vertex;
            HoveredEdge = null;
            return true;
        }

        private bool CheckEdgeSelection(Vector2 vertex, Vector2 nextVertex, Vector2 mousePosition)
        {
            var mouseToEdge = HandleUtility.DistancePointToLineSegment(mousePosition, vertex, nextVertex);
            if(mouseToEdge > Context.VertexRadius) return false;

            Changed = HoveredEdge != vertex;
            if(!Changed) return true;

            HoveredEdge = vertex;
            HoveredVertex = null;
            return true;
        }

        /// <summary>
        /// Select a vertex on any shape
        /// </summary>
        /// <param name="vertex"></param>
        public void SelectVertex(Vector2 vertex)
        {
            CurrentShape = Context.RetreiveShapeFromVertex(vertex);
            SelectedVertex = CurrentShape.GetVertexIndex(vertex);
        }
        
        /// <summary>
        /// Select a vertex on the current shape
        /// </summary>
        /// <param name="vertexIndex"></param>
        public void SelectVertex(int vertexIndex)
        {
            var vertexCount = CurrentShape.Vertices.Length;
            if(vertexCount == 0) return;

            vertexIndex = vertexIndex < 0 ? vertexCount - 1 : vertexIndex % vertexCount;
            SelectedVertex = vertexIndex;
        }

        /// <summary>
        /// Unselect all vertices
        /// </summary>
        public void ClearVertexSelection()
        {
            SelectedVertex = -1;
        }
        
        /// <summary>
        /// Unselect all shapes
        /// </summary>
        public void ClearShapeSelection()
        {
            CurrentShape = null;
        }
    }
}