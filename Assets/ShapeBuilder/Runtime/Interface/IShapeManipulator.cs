using UnityEngine;

public interface IShapeManipulator
{
    IShape[] Shapes { get; }
    int ShapeCount { get; }

    void AddShape(Vector2 position);
    void DeleteShape(int index);
    void DeleteShape(IShape shape);
    IShape GetShape(int currentShapeIndex);
    int GetShapeIndex(IShape shape);
    IShape RetreiveShapeFromVertex(Vector2 vertex);
}