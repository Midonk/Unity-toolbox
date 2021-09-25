using System.Collections.Generic;
using UnityEngine;

public class GameEvent : ScriptableObject
{
    private List<GameEventListener> _listeners = new List<GameEventListener>();
    public virtual void Raise()
    {
        for(int i = _listeners.Count - 1; i >= 0; i--)
        {
            _listeners[i].OnEventRaised();
        }
    }

    public void Register(GameEventListener listener)
    {
        if(_listeners.Contains(listener)) return;

        _listeners.Add(listener);
    }
    
    public void Unregister(GameEventListener listener)
    {
        if(!_listeners.Contains(listener)) return;

        _listeners.Remove(listener);
    }
}