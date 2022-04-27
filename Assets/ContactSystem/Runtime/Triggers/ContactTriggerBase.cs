using UnityEngine;
using UnityEngine.Events;

namespace ContactSystem
{
    public abstract class ContactTriggerBase : MonoBehaviour 
    {
        #region Exposed
        
        [Header("Filter")]
        [SerializeField] protected LayerTagFilter _filter;
        
        [Header("Triggers")]
        [SerializeField] protected UnityEvent _onEnter;
        [SerializeField] protected UnityEvent _onExit;

        #endregion


        #region Main

        /// <summary>
        ///     Handle the case where a valid element enters the collider
        /// </summary>
        [ContextMenu("On Enter")]
        protected void DetectEntrance()
        {
            _onEnter?.Invoke();
        }
        
        /// <summary>
        ///     Handle the case where a valid element exits the collider
        /// </summary>
        [ContextMenu("On Exit")]
        protected void DetectExit()
        {
            _onExit?.Invoke();
        }
             
        #endregion
    }
}