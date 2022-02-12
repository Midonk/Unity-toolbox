using UnityEngine;
using UnityEngine.Events;
using UnityEngine.EventSystems;
using UnityEngine.UI;

namespace TF.DebugMenu.Components
{
    public abstract class UIHandle<T> : Selectable, ISubmitHandler, ICancelHandler
    {
        #region Exposed

        [SerializeField] private UnityEvent _onSubmit;
        [SerializeField] private UnityEvent _onCancel;

        #endregion

        #region Properties

        public abstract T CurrentValue { get; set; }

        #endregion


        #region Unity API

        public virtual void OnSubmit(BaseEventData eventData)
        {
            _isEditing = false;
            _onSubmit?.Invoke();
        }
        
        public virtual void OnCancel(BaseEventData eventData)
        {
            if(_isEditing)
            {
                CurrentValue = _bufferedValue;
                _isEditing = false;
            }

            _onCancel?.Invoke();
        }

        #endregion


        #region Main

        /// <summary>
        /// Make the handle go step times to left
        /// </summary>
        /// <param name="step">Number of time the handle value goes left</param>
        public abstract void Decrement(T step);

        /// <summary>
        /// Make the handle go step times to right
        /// </summary>
        /// <param name="step">Number of time the handle value goes right</param>
        public abstract void Increment(T step);

        #endregion

        
        #region Utils

        protected void BufferValue()
        {
            if (_isEditing || currentSelectionState != SelectionState.Selected) return;

            _isEditing = true;
            _bufferedValue = CurrentValue;
        }
            
        #endregion

        #region Pirvate Fields

        protected bool _isEditing;
        protected T _currentValue;
        private T _bufferedValue;
            
        #endregion
    }
}