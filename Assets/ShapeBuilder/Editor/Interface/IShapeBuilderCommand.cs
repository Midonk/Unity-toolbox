using UnityEngine;

namespace Thomas.Test.New
{
    public interface IShapeBuilderCommand
    {
        bool Undoable { get; }
        bool NeedRepaint { get; }
        void Execute(IShapeSelectionInfo selection, Vector2 mousePosition, IShapeManipulator builder);
    }
}