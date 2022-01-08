using System;
using System.Collections.Generic;
using UnityEngine;

namespace Thomas.Test.New
{
    public class Shape : IShape<Vector2>
    {
        public Vector2[] Vertices => _vertices.ToArray();

        public void AddVertex(Vector2 position) => _vertices.Add(position);

        public Vector2 GetVertex(int vertexIndex) => _vertices[vertexIndex];
        
        public int GetVertexIndex(Vector2 vertex) => _vertices.IndexOf(vertex);

        public void RemoveVertex(int vertexIndex) => _vertices.RemoveAt(vertexIndex);

        public void UpdateVertex(int vertexIndex, Vector2 position) => _vertices[vertexIndex] = position;

        public bool Contains(Vector2 vertex) => _vertices.Contains(vertex);

        private List<Vector2> _vertices = new List<Vector2>();

        public void AddVertex(int index, Vector2 position) => _vertices.Insert(index, position);
    }
}