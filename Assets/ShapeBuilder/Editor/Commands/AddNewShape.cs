using Thomas.Test.New;
using UnityEngine;

[CreateAssetMenu(menuName = "ShapeBuilder/Commands/Add shape", fileName = "Add shape")]
public class AddNewShape : Command, IShapeBuilderCommand
{
    public bool Undoable => true;
    public bool NeedRepaint => true;

    public void Execute(SelectionInfo selection, Vector2 mousePosition, ShapeBuilder builder)
    {
        builder.AddShape(mousePosition);
        selection.SelectVertex(mousePosition);
    }
}