using UnityEngine;

[CreateAssetMenu(menuName = "ShapeBuilder/Commands/Add vertex", fileName = "Add vertex")]
public class AddVertex : Command, IShapeBuilderCommand
{
    public bool Undoable => true;
    public bool NeedRepaint => true;

    public void Execute(IShapeSelectionInfo selection, Vector2 mousePosition, IShapeManipulator builder)
    {
        if (selection.MouseHoveringVertex || selection.MouseHoveringEdge) return;
        
        if (builder.ShapeCount == 0 || selection.CurrentShape is null)
        {
            builder.AddShape(mousePosition);
        }
        
        else
        {
            var selectedVertex = selection.SelectedVertex;
            selection.CurrentShape.InsertVertex(selectedVertex + 1, mousePosition);
        }

        selection.SelectVertex(mousePosition);
    }
}