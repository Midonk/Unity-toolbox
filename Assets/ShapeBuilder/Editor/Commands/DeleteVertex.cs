using Thomas.Test.New;
using UnityEngine;

[CreateAssetMenu(menuName = "ShapeBuilder/Commands/Delete vertex", fileName = "Delete vertex")]
public class DeleteVertex : Command, IShapeBuilderCommand
{
    public bool Undoable => true;

    public bool NeedRepaint => true;

    public void Execute(SelectionInfo selection, Vector2 mousePosition, ShapeBuilder builder)
    {
        if(selection.SelectedVertex == -1) return;
        
        var vertex = selection.SelectedVertex;
        var shape = selection.CurrentShape;
        shape.RemoveVertex(vertex);
        if(shape.Vertices.Length > 0)
        {
            selection.SelectVertex(vertex - 1);
        }

        else
        {
            builder.DeleteShape(shape);
            selection.ClearVertexSelection();
            selection.ClearShapeSelection();
        }
    }
}