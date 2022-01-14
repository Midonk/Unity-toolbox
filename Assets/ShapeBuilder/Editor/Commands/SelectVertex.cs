using UnityEngine;

[CreateAssetMenu(menuName = "ShapeBuilder/Commands/Select vertex", fileName = "Select vertex")]
public class SelectVertex : Command, IShapeBuilderCommand
{
    public bool Undoable => true;
    public bool NeedRepaint => false;

    public void Execute(IShapeSelectionInfo selection, Vector2 mousePosition, IShapeManipulator builder)
    {
        if(!selection.MouseHoveringVertex) return;
        
        var vertex = selection.HoveredVertex ?? Vector2.zero;
        selection.SelectVertex(vertex);
    }
}