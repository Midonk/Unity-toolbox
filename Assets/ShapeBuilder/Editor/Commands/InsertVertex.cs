using UnityEngine;

namespace Thomas.Test.New
{
    [CreateAssetMenu(menuName = "ShapeBuilder/Commands/Insert vertex", fileName = "Insert vertex")]
    public class InsertVertex : Command, IShapeBuilderCommand
    {
        public bool Undoable => true;
        public bool NeedRepaint => true;

        public void Execute(SelectionInfo selection, Vector2 mousePosition, ShapeBuilder builder)
        {
            if(!selection.MouseHoveringEdge) return;
            
            var edge = selection.HoveredEdge ?? Vector2.zero ;
            var shape = builder.RetreiveShapeFromVertex(edge);
            var edgeIndex = shape.GetVertexIndex(edge);
            shape.AddVertex(edgeIndex + 1, mousePosition);
            selection.SelectVertex(mousePosition);
        }
    }
}