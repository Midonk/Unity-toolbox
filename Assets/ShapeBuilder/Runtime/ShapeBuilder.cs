using System.Collections.Generic;
using UnityEngine;

namespace Thomas.Test.New
{
    public class ShapeBuilder : MonoBehaviour, IShapeManipulator
    {
        [SerializeField] private EditorInputTrigger _inputTrigger;

        public EditorInputTrigger InputTrigger => _inputTrigger;
        public List<IShape> Shapes => _shapes;

        public void AddShape(Vector2 position)
        {
            var shape = new Shape();
            shape.AddVertex(position);
            _shapes.Add(shape);
        }

        public IShape RetreiveShapeFromVertex(Vector2 vertex)
        {
            for (int i = 0; i < _shapes.Count; i++)
            {
                var shape = _shapes[i];
                if (!shape.Has(vertex)) continue;

                return shape;
            }

            return null;
        }

        public int GetShapeIndex(IShape shape) => _shapes.IndexOf(shape);

        public void DeleteShape(int index) => _shapes.RemoveAt(index);

        public void DeleteShape(IShape shape) => _shapes.Remove(shape);

        private List<IShape> _shapes = new List<IShape>();
    }
}