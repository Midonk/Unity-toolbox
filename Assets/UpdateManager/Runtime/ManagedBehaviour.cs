using UnityEngine;

public abstract class ManagedBehaviour : MonoBehaviour
{
    protected virtual void Awake() 
    {
        if(this is IUpdatableBehaviour)
        {
            UpdateManager.Instance.Register((IUpdatableBehaviour)this);
        }
    }

    protected virtual void OnDestroy() 
    {
        if(this is IUpdatableBehaviour)
        {
            UpdateManager.Instance.Unregister((IUpdatableBehaviour)this);
        }
    }
}