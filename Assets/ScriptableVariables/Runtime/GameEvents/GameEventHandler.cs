using UnityEngine;
using UnityEngine.Events;

namespace ScriptableVariables
{
    public class GameEventHandler : MonoBehaviour
    {
        #region Exposed

        [SerializeField]
        private Listener[] _listeners;
             
        #endregion


        #region Unity API

        private void OnEnable() 
        {
            foreach (var listener in _listeners)
            {
                listener.Event.Register(listener);
            }    
        }

        private void OnDisable() 
        {
            foreach (var listener in _listeners)
            {
                listener.Event.Unregister(listener);
            }    
        }
             
        #endregion


        [System.Serializable]
        private class Listener : IGameEventListener
        {
            [SerializeField]
            private GameEvent _event;
            
            [SerializeField]
            private UnityEvent _response;

            public GameEvent Event => _event;

            public void OnEventRaised()
            {
                _response?.Invoke();
            }
        }
    }
}