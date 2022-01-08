namespace Thomas.Test.New
{
    public interface IShape<T>
    {
        T[] Vertices { get; }

        void AddVertex(T position);
        void RemoveVertex(int vertexIndex);
        T GetVertex(int vertexIndex);
        bool Contains(T vertex);
        void UpdateVertex(int vertexIndex, T position);
    }
}