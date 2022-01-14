using UnityEngine;

public interface IShapeSelectionInfo : ISelectionInfo<IShapeManipulator>
{
    bool MouseHoveringVertex { get; }
    bool MouseHoveringEdge { get; }
    Vector2? HoveredVertex { get; }
    Vector2? HoveredEdge { get; }
    IShape CurrentShape { get; }
    int CurrentShapeIndex { get; }
    int SelectedVertex { get; }
    float VertexRadius { get; }

    void ClearShapeSelection();
    void ClearVertexSelection();
    void SelectShape(int shapeIndex);
    void SelectVertex(Vector2 vertex);
    void SelectVertex(int vertexIndex);
}