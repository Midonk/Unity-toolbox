using System;
using System.Collections.Generic;
using UnityEngine;

//CLOSED

public class Shape : IShape
{
    #region Properties

    public Vector2[] Vertices => _vertices.ToArray();
    public int VertexCount => _vertices.Count;

    #endregion


    #region Main

    public void AddVertex(Vector2 position) => _vertices.Add(position);
    
    public void InsertVertex(int index, Vector2 position) => _vertices.Insert(index, position);

    public void RemoveVertex(int vertexIndex) => _vertices.RemoveAt(vertexIndex);

    public void UpdateVertex(int vertexIndex, Vector2 position) => _vertices[vertexIndex] = position;

    #endregion

    
    #region Utils

    public Vector2 GetVertex(int vertexIndex) => _vertices[vertexIndex];
    
    public int GetVertexIndex(Vector2 vertex) => _vertices.IndexOf(vertex);
    
    public bool Has(Vector2 vertex) => _vertices.Contains(vertex);
            
    #endregion


    #region Private Fields

    private List<Vector2> _vertices = new List<Vector2>();

    #endregion
}