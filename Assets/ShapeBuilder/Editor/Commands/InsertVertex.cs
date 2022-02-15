using UnityEngine;

[CreateAssetMenu(menuName = "ShapeBuilder/Commands/Insert vertex", fileName = "Insert vertex")]
public class InsertVertex : Command, IShapeBuilderCommand
{
    public bool Undoable => true;
    public bool NeedRepaint => true;

    public void Execute(IShapeSelectionInfo selection, Vector2 mousePosition, IShapeManipulator builder)
    {
        if(!selection.MouseHoveringEdge) return;
        
        var edge = selection.HoveredEdge ?? Vector2.zero ;
        var shape = builder.RetreiveShapeFromVertex(edge);
        var edgeIndex = shape.GetVertexIndex(edge);
        shape.InsertVertex(edgeIndex + 1, mousePosition);
        selection.SelectVertex(mousePosition);
    }
}