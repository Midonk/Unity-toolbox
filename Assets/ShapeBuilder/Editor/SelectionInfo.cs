using UnityEngine;
using UnityEditor;

public class SelectionInfo : IShapeSelectionInfo
{
    public SelectionInfo(IShapeManipulator context)
    {
        Context = context;
    }


    #region Properties

    public bool MouseHoveringVertex => HoveredVertex != null;
    public bool MouseHoveringEdge => HoveredEdge != null;
    public Vector2? HoveredVertex { get; private set; }
    //vertex that makes the edge with vertex + 1
    public Vector2? HoveredEdge { get; private set; }
    public IShape CurrentShape { get; private set; }
    public int CurrentShapeIndex { get; private set; } = -1;
    public int SelectedVertex { get; private set; } = -1;
    public IShapeManipulator Context { get; private set; }
    public bool Changed { get; private set; }
    public float VertexRadius => 0.1f;

    #endregion


    #region Main

    public void UpdateSelection(Vector2 mousePosition)
    {
        var shapes = Context.Shapes;
        for (int i = 0; i < Context.ShapeCount; i++)
        {
            var shape = shapes[i];
            var vertxCount = shape.VertexCount;

            //vertex selection
            for (int j = 0; j < vertxCount; j++)
            {
                var vertex = shape.GetVertex(j);
                var isVertexSelected = CheckVertexSelection(vertex, mousePosition);
                if (isVertexSelected) return;
            }

            //edge selection
            for (int j = 0; j < vertxCount; j++)
            {
                var vertex = shape.GetVertex(j);
                var nextVertex = shape.GetVertex((j + 1) % vertxCount);
                var isEdgeSelected = CheckEdgeSelection(vertex, nextVertex, mousePosition);
                if (isEdgeSelected) return;
            }

            //nothing selected
            Changed = HoveredVertex != null || HoveredEdge != null;
            HoveredVertex = null;
            HoveredEdge = null;
        }
    }

    #endregion


    #region Plumbery

    private bool CheckVertexSelection(Vector2 vertex, Vector2 mousePosition)
    {
        var mouseToVertex = vertex - mousePosition;
        if (mouseToVertex.sqrMagnitude > VertexRadius * VertexRadius) return false;

        Changed = vertex != HoveredVertex;
        if (!Changed) return true;

        HoveredVertex = vertex;
        HoveredEdge = null;
        return true;
    }

    private bool CheckEdgeSelection(Vector2 vertex, Vector2 nextVertex, Vector2 mousePosition)
    {
        var mouseToEdge = HandleUtility.DistancePointToLineSegment(mousePosition, vertex, nextVertex);
        if (mouseToEdge > VertexRadius) return false;

        Changed = HoveredEdge != vertex;
        if (!Changed) return true;

        HoveredEdge = vertex;
        HoveredVertex = null;
        return true;
    }

    #endregion


    #region Utils

    /// <summary>
    /// Select a vertex on any shape
    /// </summary>
    /// <param name="vertex">Vertex to select</param>
    public void SelectVertex(Vector2 vertex)
    {
        CurrentShape = Context.RetreiveShapeFromVertex(vertex);
        CurrentShapeIndex = Context.GetShapeIndex(CurrentShape);
        SelectedVertex = CurrentShape.GetVertexIndex(vertex);
    }

    /// <summary>
    /// Select a vertex on the current shape
    /// </summary>
    /// <param name="vertexIndex">Index of the vertex to select</param>
    public void SelectVertex(int vertexIndex)
    {
        var vertexCount = CurrentShape.VertexCount;
        if (vertexCount == 0) return;

        vertexIndex = (int)Mathf.Repeat(vertexIndex, vertexCount);
        vertexIndex = Mathf.Max(-1, vertexIndex);
        SelectedVertex = vertexIndex;
    }

    /// <summary>
    /// Select a Shape
    /// </summary>
    /// <param name="shapeIndex">Index of the shape to select</param>
    public void SelectShape(int shapeIndex)
    {
        var shapeCount = Context.ShapeCount;
        CurrentShapeIndex = (int)Mathf.Repeat(shapeIndex, shapeCount);
        if(CurrentShapeIndex < 0)
        {
            ClearShapeSelection();
            return;
        }
        
        CurrentShape = Context.GetShape(CurrentShapeIndex);
    }

    /// <summary>
    /// Unselect all vertices
    /// </summary>
    public void ClearVertexSelection()
    {
        SelectedVertex = -1;
    }

    /// <summary>
    /// Unselect all shapes and vertices
    /// </summary>
    public void ClearShapeSelection()
    {
        CurrentShape = null;
        CurrentShapeIndex = -1;
        ClearVertexSelection();
    }

    #endregion
}