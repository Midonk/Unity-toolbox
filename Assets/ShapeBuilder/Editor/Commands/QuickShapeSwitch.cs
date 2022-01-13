

using Thomas.Test.New;
using UnityEngine;

[CreateAssetMenu(menuName = "ShapeBuilder/Commands/Switch shape", fileName = "Switch shape")]
public class QuickShapeSwitch : Command, IShapeBuilderCommand
{
    public bool Undoable => false;
    public bool NeedRepaint => true;

    public void Execute(IShapeSelectionInfo selection, Vector2 mousePosition, IShapeManipulator builder)
    {
        if(selection.CurrentShape is null) return;
        
        selection.SelectShape(selection.CurrentShapeIndex + 1);
        selection.SelectVertex(0);
    }
}