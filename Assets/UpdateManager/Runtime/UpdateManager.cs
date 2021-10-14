using Utils;
using UnityEngine;

public class UpdateManager
{
    private UpdateManager() {}

    #region Properties

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
         
    #endregion


    #region Main

    /// <summary>
    ///     Update registered updatables based on delta time
    /// </summary>
    public virtual void Tick()
    {
        for (int i = _updatables.Count - 1; i >= 0; i--)
        {
            var behaviour = _updatables[i];
            if(!CheckSchedule(behaviour)) return;
            behaviour.Tick();
        }
    }

    /// <summary>
    ///     Update registered fixed updatables based on fixed delta time
    /// </summary>    
    public virtual void FixedTick()
    {
        for (int i = _fixedUpdatables.Count - 1; i >= 0; i--)
        {
            var behaviour = _fixedUpdatables[i];
            if(!CheckSchedule(behaviour)) return;
            behaviour.FixedTick();
        }
    }

    /// <summary>
    ///     Register a behaviour to the appropriates update lists
    /// </summary>
    /// <param name="behaviour">Behaviour to register</param>
    public void Register(IUpdatableBehaviour behaviour)
    {
        if(behaviour is IUpdatable updatable)
        {
            _updatables.Add(updatable);
        }
        
        if(behaviour is IFixedUpdatable fixedUpdatable)
        {
            _fixedUpdatables.Add(fixedUpdatable);
        }
    }

    /// <summary>
    ///     Unregister a behaviour from the appropriates update lists
    /// </summary>
    /// <param name="behaviour">Behaviour to unregister</param>
    public void Unregister(IUpdatableBehaviour behaviour)
    {
        if(behaviour is IUpdatable updatable)
        {
            _updatables.Remove(updatable);
        }
        
        if(behaviour is IFixedUpdatable fixedUpdatable)
        {
            _fixedUpdatables.Remove(fixedUpdatable);
        }
    }
      
    #endregion

    
    #region Utils

    private bool CheckSchedule(IUpdatableBehaviour behaviour)
    {
        //maybe cache time.framecount if more optimised ?
        if(!(behaviour is IScheduledUpdate scheduled)) return true;            
        if(Time.frameCount % scheduled.Interval != 0) return false;

        return true;    
    }
    
    #endregion

    
    #region Private Fields

    private static UpdateManager _instance;
    private SmartList<IUpdatable> _updatables = new SmartList<IUpdatable>();
    private SmartList<IFixedUpdatable> _fixedUpdatables = new SmartList<IFixedUpdatable>();
         
    #endregion
}