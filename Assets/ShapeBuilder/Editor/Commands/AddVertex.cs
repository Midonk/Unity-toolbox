using UnityEngine;

namespace Thomas.Test.New
{
    [CreateAssetMenu(menuName = "ShapeBuilder/Commands/Add vertex", fileName = "Add vertex")]
    public class AddVertex : Command, IShapeBuilderCommand
    {
        public bool Undoable => true;
        public bool NeedRepaint => true;

        public void Execute(SelectionInfo selection, Vector2 mousePosition, ShapeBuilder builder)
        {
            if (selection.MouseHoveringVertex || selection.MouseHoveringEdge) return;
            
            if (builder.Shapes.Length == 0 || selection.CurrentShape is null)
            {
                builder.AddShape(mousePosition);
            }
            
            else
            {
                var selectedVertex = selection.SelectedVertex;
                selection.CurrentShape.AddVertex(selectedVertex + 1, mousePosition);
            }

            selection.SelectVertex(mousePosition);
        }
    }
}