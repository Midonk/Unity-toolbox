using Thomas.Test.New;
using UnityEngine;

[CreateAssetMenu(menuName = "ShapeBuilder/Commands/Select next vertex", fileName = "Select next vertex")]
public class SelectNextVertex : Command, IShapeBuilderCommand
{
    public bool Undoable => false;

    public bool NeedRepaint => true;

    public void Execute(IShapeSelectionInfo selection, Vector2 mousePosition, IShapeManipulator builder)
    {
        var vertex = selection.SelectedVertex;
        selection.SelectVertex(vertex + 1);
    }
}