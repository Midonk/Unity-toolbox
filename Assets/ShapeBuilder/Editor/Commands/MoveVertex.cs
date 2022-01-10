using UnityEngine;

namespace Thomas.Test.New
{
    [CreateAssetMenu(menuName = "ShapeBuilder/Commands/Move vertex", fileName = "Move vertex")]
    public class MoveVertex : Command, IShapeBuilderCommand
    {
        public bool Undoable { get; } = false;
        public bool NeedRepaint { get; } = true;

        public void Execute(SelectionInfo selection, Vector2 mousePosition, ShapeBuilder builder)
        {
            var vertex = selection.SelectedVertex;
            if(vertex == -1) return;
            
            selection.CurrentShape.UpdateVertex(vertex, mousePosition);
        }
    }
}