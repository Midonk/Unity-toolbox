using UnityEngine;

namespace Thomas.Test.New
{
    public interface IShapeBuilderCommand
    {
        bool Undoable { get; }
        bool NeedRepaint { get; }
        void Execute(SelectionInfo selection, Vector2 mousePosition, ShapeBuilder builder);
    }
}