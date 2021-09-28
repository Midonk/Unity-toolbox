using UnityEngine;
using UnityEngine.Events;

namespace ScriptableVariables
{
    public class GameEventListener : MonoBehaviour, IGameEventListener
    {
        #region Exposed

        [SerializeField]
        private GameEvent _event;
        
        [SerializeField]
        private UnityEvent _response;
             
        #endregion

        
        #region Unity API

        private void OnEnable() 
        {
            _event.Register(this);    
        }

        private void OnDisable() 
        {
            _event.Unregister(this);    
        }

             
        #endregion
        
        
        #region Main

        public void OnEventRaised()
        {
            _response?.Invoke();
        }
             
        #endregion
    }
}