using UnityEngine;
using UnityEngine.Events;

namespace ContactSystem
{
    [RequireComponent(typeof(Collider))]
    public class ContactHandler : ContactBase
    {
        #region Exposed
        
        [SerializeField]
        private UnityEvent _onEnter;
        
        [SerializeField]
        private UnityEvent _onExit;

        #endregion


        #region Unity API

        private void OnTriggerEnter(Collider other) 
        {
            if(!PassFilter(other.gameObject)) return;

            _onEnter?.Invoke();
        }
        
        private void OntriggerExit(Collider other) 
        {
            if(!PassFilter(other.gameObject)) return;

            _onExit?.Invoke();
        }

        private void OnCollisionEnter(Collision other) 
        {
            if(!PassFilter(other.gameObject)) return;

            _onEnter?.Invoke();
        }

        private void OnCollisionExit(Collision other) 
        {
            if(!PassFilter(other.gameObject)) return;

            _onExit?.Invoke();
        }

        #endregion
    }
}