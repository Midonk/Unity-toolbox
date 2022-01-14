using UnityEngine;

[CreateAssetMenu(menuName = "ShapeBuilder/Commands/Add shape", fileName = "Add shape")]
public class AddNewShape : Command, IShapeBuilderCommand
{
    public bool Undoable => true;
    public bool NeedRepaint => true;

    public void Execute(IShapeSelectionInfo selection, Vector2 mousePosition, IShapeManipulator builder)
    {
        builder.AddShape(mousePosition);
        selection.SelectVertex(mousePosition);
    }
}