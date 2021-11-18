using UnityEngine;

//Base class for custom behaviours

public abstract class ManagedBehaviour : MonoBehaviour
{
    protected virtual void Awake() 
    {
        if(this is IUpdatableBehaviour behaviour)
        {
            UpdateManager.Instance.Register(behaviour);
        }
    }

    protected virtual void OnDestroy() 
    {
        if(this is IUpdatableBehaviour behaviour)
        {
            UpdateManager.Instance.Unregister(behaviour);
        }
    }
}