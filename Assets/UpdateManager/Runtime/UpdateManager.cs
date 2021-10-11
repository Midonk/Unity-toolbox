using Utils;
using UnityEngine;

public class UpdateManager
{
    protected UpdateManager() {}

    public static UpdateManager Instance
    {
        get
        {
            if(_instance == null)
            {
                _instance = new UpdateManager();
            }

            return _instance;
        }
    }

    public virtual void Tick()
    {
        for (int i = _updatables.Count - 1; i >= 0; i--)
        {
            _updatables[i].Tick();
        }
    }
    
    public virtual void FixedTick()
    {
        for (int i = _fixedUpdatables.Count - 1; i >= 0; i--)
        {
            _fixedUpdatables[i].FixedTick();
        }
    }

    public void Add(IUpdatableBehaviour behaviour)
    {
        if(behaviour is IUpdatable)
        {
            _updatables.Add((IUpdatable)behaviour);
        }
        
        if(behaviour is IFixedUpdatable)
        {
            _fixedUpdatables.Add((IFixedUpdatable)behaviour);
        }
    }

    public void Remove(IUpdatableBehaviour behaviour)
    {
        if(behaviour is IUpdatable)
        {
            _updatables.Remove((IUpdatable)behaviour);
        }
        
        if(behaviour is IFixedUpdatable)
        {
            _fixedUpdatables.Remove((IFixedUpdatable)behaviour);
        }
    }

    private static UpdateManager _instance;
    private SmartList<IUpdatable> _updatables = new SmartList<IUpdatable>();
    private SmartList<IFixedUpdatable> _fixedUpdatables = new SmartList<IFixedUpdatable>();
}