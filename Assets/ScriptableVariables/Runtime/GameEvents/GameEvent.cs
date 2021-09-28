using System.Collections.Generic;
using UnityEngine;

namespace ScriptableVariables
{
    public class GameEvent : ScriptableObject
    {
        #region Main

        public virtual void Raise()
        {
            for(int i = _listeners.Count - 1; i >= 0; i--)
            {
                _listeners[i].OnEventRaised();
            }
        }

        public void Register(IGameEventListener listener)
        {
            if(_listeners.Contains(listener)) return;

            _listeners.Add(listener);
        }
        
        public void Unregister(IGameEventListener listener)
        {
            if(!_listeners.Contains(listener)) return;

            _listeners.Remove(listener);
        }
             
        #endregion


        #region Private Fields

        private List<IGameEventListener> _listeners = new List<IGameEventListener>();
             
        #endregion        
    }
}