using UnityEngine;

namespace Thomas.Test.New
{
    public interface ISelectionInfo<T>
    {
        T Context { get; }
        bool Changed { get; }
        
        void UpdateSelection(Vector2 mousePosition);
    }
}