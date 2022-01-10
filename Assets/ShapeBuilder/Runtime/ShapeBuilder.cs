using System.Collections.Generic;
using UnityEngine;

namespace Thomas.Test.New
{
    public class ShapeBuilder : MonoBehaviour 
    {
        [SerializeField] private EditorInputTrigger _inputTrigger;

        [Header("Visuals")]
        [Min(0.001f)]
        [SerializeField] private float _vertexRadius = 0.1f;

        public EditorInputTrigger InputTrigger => _inputTrigger;
        public Shape[] Shapes => _shapes.ToArray();
        public float VertexRadius => _vertexRadius;

        public void AddShape(Vector2 position)
        {
            var shape = new Shape();
            shape.AddVertex(position);
            _shapes.Add(shape);
        }

        public Shape RetreiveShapeFromVertex(Vector2 vertex)
        {
            for (int i = 0; i < _shapes.Count; i++)
            {
                var shape = _shapes[i];
                if(!shape.Contains(vertex)) continue;

                return shape;
            }

            return null;
        }

        public void DeleteShape(int index) => _shapes.RemoveAt(index);
        public void DeleteShape(Shape shape) => _shapes.Remove(shape);

        private List<Shape> _shapes = new List<Shape>();
    }
}