using Thomas.Test.New;
using UnityEngine;

[CreateAssetMenu(menuName = "ShapeBuilder/Commands/Delete vertex", fileName = "Delete vertex")]
public class DeleteVertex : Command, IShapeBuilderCommand
{
    public bool Undoable => true;

    public bool NeedRepaint => true;

    public void Execute(IShapeSelectionInfo selection, Vector2 mousePosition, IShapeManipulator builder)
    {
        if(selection.SelectedVertex == -1) return;
        
        var vertex = selection.SelectedVertex;
        var shape = selection.CurrentShape;
        shape.RemoveVertex(vertex);
        if(shape.Vertices.Count > 0)
        {
            selection.SelectVertex(vertex - 1);
        }

        else
        {
            var shapeIndex = builder.GetShapeIndex(shape);
            builder.DeleteShape(shape);
            selection.SelectShape(shapeIndex - 1);
            selection.ClearVertexSelection();
        }
    }
}