using UnityEngine;

public abstract class ManagedBehaviour : MonoBehaviour
{
    protected virtual void Awake() 
    {
        var updateManager = UpdateManager.Instance;
        if(this is IUpdatableBehaviour)
        {
            updateManager.Add((IUpdatableBehaviour)this);
        }
    }
}