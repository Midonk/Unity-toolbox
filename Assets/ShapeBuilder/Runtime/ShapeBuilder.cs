using System.Collections.Generic;
using UnityEngine;

public class ShapeBuilder : IShapeManipulator
{
    #region Properties

    public IShape[] Shapes => _shapes.ToArray();
    public int ShapeCount => _shapes.Count;

    #endregion


    #region Main

    public void AddShape(Vector2 position)
    {
        var shape = new Shape();
        shape.AddVertex(position);
        _shapes.Add(shape);
    }

    public void DeleteShape(int index) => _shapes.RemoveAt(index);

    public void DeleteShape(IShape shape) => _shapes.Remove(shape);
    
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

    public IShape GetShape(int shapeIndex) => _shapes[shapeIndex];
         
    #endregion


    private List<IShape> _shapes = new List<IShape>();
}