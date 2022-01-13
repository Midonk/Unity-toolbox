using System.Collections.Generic;
using UnityEngine;

namespace Thomas.Test.New
{
    public interface IShapeManipulator
    {
        List<IShape> Shapes { get; }

        void AddShape(Vector2 position);
        void DeleteShape(int index);
        void DeleteShape(IShape shape);
        int GetShapeIndex(IShape shape);
        IShape RetreiveShapeFromVertex(Vector2 vertex);
    }
}