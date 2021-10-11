using Utils;
using UnityEngine;

public class UpdateManager
{
    private UpdateManager() {}

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

    /// <summary>
    ///     Update registered updatables based on delta time
    /// </summary>
    public virtual void Tick()
    {
        for (int i = _updatables.Count - 1; i >= 0; i--)
        {
            _updatables[i].Tick();
        }
    }

    /// <summary>
    ///     Update registered fixed updatables based on fixed delta time
    /// </summary>    
    public virtual void FixedTick()
    {
        for (int i = _fixedUpdatables.Count - 1; i >= 0; i--)
        {
            _fixedUpdatables[i].FixedTick();
        }
    }

    /// <summary>
    ///     Register a behaviour to the appropriates update lists
    /// </summary>
    /// <param name="behaviour">Behaviour to register</param>
    public void Register(IUpdatableBehaviour behaviour)
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

    /// <summary>
    ///     Unregister a behaviour from the appropriates update lists
    /// </summary>
    /// <param name="behaviour">Behaviour to unregister</param>
    public void Unregister(IUpdatableBehaviour behaviour)
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