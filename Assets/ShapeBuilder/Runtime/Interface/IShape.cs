using System.Collections.Generic;
using UnityEngine;

public interface IShape
{
    Vector2[] Vertices { get; }
    int VertexCount { get; }

    void AddVertex(Vector2 position);
    void InsertVertex(int index, Vector2 position);
    void RemoveVertex(int vertexIndex);
    Vector2 GetVertex(int vertexIndex);
    int GetVertexIndex(Vector2 vertex);
    bool Has(Vector2 vertex);
    void UpdateVertex(int vertexIndex, Vector2 position);
}