using UnityEngine;

[CreateAssetMenu(menuName = "ShapeBuilder/Commands/Select previous vertex", fileName = "Select previous vertex")]
public class SelectPreviousVertex : Command, IShapeBuilderCommand
{
    public bool Undoable => false;
    public bool NeedRepaint => true;

    public void Execute(IShapeSelectionInfo selection, Vector2 mousePosition, IShapeManipulator builder)
    {
        var vertex = selection.SelectedVertex;
        selection.SelectVertex(vertex - 1);
    }
}