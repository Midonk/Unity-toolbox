using Thomas.Test.New;
using UnityEngine;

[CreateAssetMenu(menuName = "ShapeBuilder/Commands/Delete hovered vertex", fileName = "Delete hovered vertex")]
public class DeleteHoveredVertex : Command, IShapeBuilderCommand
{
    public bool Undoable => true;
    public bool NeedRepaint => true;

    public void Execute(IShapeSelectionInfo selection, Vector2 mousePosition, IShapeManipulator builder)
    {
        if(!selection.MouseHoveringVertex) return;

        var vertex = selection.HoveredVertex ?? Vector2.zero;
        var shape = builder.RetreiveShapeFromVertex(vertex);
        var vertexIndex = shape.GetVertexIndex(vertex);
        
        shape.RemoveVertex(vertexIndex);
        if(vertexIndex == selection.SelectedVertex)
        {
            selection.ClearVertexSelection();
        }

        if(shape.Vertices.Count > 0) return;

        builder.DeleteShape(shape);
        if(selection.CurrentShape != shape) return;
        
        selection.ClearShapeSelection();
    }
}