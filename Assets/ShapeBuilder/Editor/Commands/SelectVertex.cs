using UnityEngine;

namespace Thomas.Test.New
{
    [CreateAssetMenu(menuName = "ShapeBuilder/Commands/Select vertex", fileName = "Select vertex")]
    public class SelectVertex : Command, IShapeBuilderCommand
    {
        public bool Undoable { get; } = true;
        public bool NeedRepaint { get; } = false;

        public void Execute(SelectionInfo selection, Vector2 mousePosition, ShapeBuilder builder)
        {
            if(selection.MouseHoveringVertex)
            {
                var vertex = selection.HoveredVertex ?? Vector2.zero;
                selection.SelectVertex(vertex);
            }
        }
    }
}