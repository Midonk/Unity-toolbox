using UnityEngine;

public interface ISelectionInfo<T>
{
    T Context { get; }
    bool Changed { get; }
    
    /// <summary>
    /// Internaly update selection given a position
    /// </summary>
    /// <param name="position"></param>
    void UpdateSelection(Vector2 position);
}