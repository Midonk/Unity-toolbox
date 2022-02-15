using UnityEngine;

public abstract class TouchBehaviour : ScriptableObject
{
    #region Properties

    public bool IsActive => _isActive;
         
    #endregion

    
    #region Main

    /// <summary>
    ///     Perform setup operations and activate this behaviour
    /// </summary>
    public virtual void Activate()
    {
        Debug.Log($"{name} is activated");
        _isActive = true;
    }

    /// <summary>
    ///     Allow the behaviour to run
    /// </summary>
    /// <param name="dt">delta time between two frames</param>
    public abstract void Tick(float dt);

    /// <summary>
    ///     Perform cleanup operations and deactivate the behaviour
    /// </summary>
    public virtual void Deactivate()
    {
        Debug.Log($"{name} is deactivated");
        _isActive = false;
    }

    #endregion

    #region Utils

    /// <summary>
    ///     Verify the passed touch count is sufficient for this behaviour
    /// </summary>
    /// <param name="touchCount">Number of touch on the screen</param>
    /// <returns>Either the touch count is valid or not</returns>
    public abstract bool HasEnoughTouch(int touchCount);

    #endregion

    
    #region Private Fields

    private bool _isActive;

    #endregion
}