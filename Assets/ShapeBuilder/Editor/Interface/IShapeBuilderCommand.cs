using UnityEngine;

public interface IShapeBuilderCommand
{
    bool Undoable { get; }
    bool NeedRepaint { get; }

    /// <summary>
    /// Execute the command logic
    /// </summary>
    /// <param name="selection"></param>
    /// <param name="mousePosition"></param>
    /// <param name="builder"></param>
    void Execute(IShapeSelectionInfo selection, Vector2 mousePosition, IShapeManipulator builder);
}