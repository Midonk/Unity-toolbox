using UnityEngine;

[CreateAssetMenu(menuName = "ShapeBuilder/Commands/Move vertex", fileName = "Move vertex")]
public class MoveVertex : Command, IShapeBuilderCommand
{
    public bool Undoable => false;
    public bool NeedRepaint => true;

    public void Execute(IShapeSelectionInfo selection, Vector2 mousePosition, IShapeManipulator builder)
    {
        
        var vertex = selection.SelectedVertex;
        if(vertex == -1) return;
        
        selection.CurrentShape.UpdateVertex(vertex, mousePosition);
    }
}