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

            DetectEntrance();
        }
        
        private void OntriggerExit(Collider other) 
        {
            if(!PassFilter(other.gameObject)) return;

            DetectExit();
        }

        private void OnCollisionEnter(Collision other) 
        {
            if(!PassFilter(other.gameObject)) return;

            DetectEntrance();
        }

        private void OnCollisionExit(Collision other) 
        {
            if(!PassFilter(other.gameObject)) return;

            DetectExit();
        }

        #endregion
    

        #region Main

        /// <summary>
        ///     Handle the case where a valid element enters the collider
        /// </summary>
        [ContextMenu("Trigger enter")]
        protected virtual void DetectEntrance()
        {
            _onEnter?.Invoke();
        }
        
        /// <summary>
        ///     Handle the case where a valid element exits the collider
        /// </summary>
        [ContextMenu("Trigger exit")]
        protected virtual void DetectExit()
        {
            _onExit?.Invoke();
        }
             
        #endregion
    }
}