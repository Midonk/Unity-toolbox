using UnityEngine;
using UnityEngine.UI;
using TF.DebugMenu.Utils;

namespace TF.DebugMenu.Components
{    
    public class NumberHandle : UIHandle<int>
    {
        #region Exposed

        [SerializeField] private InputField _field;
            
        #endregion

        
        #region Properties

        public override int CurrentValue
        {
            get => _currentValue;
            set
            {
                _currentValue = value;
                _field.text = _currentValue.ToString();
            }
        }

        #endregion

        
        #region Unity API

        protected override void Awake() 
        {
            base.Awake();
            _field.onEndEdit.AddListener(SetNumber);
        }

        private void OnGUI()
        {
            var evt = Event.current;
            if (currentSelectionState != SelectionState.Selected) return;
            if (evt.type != EventType.KeyDown) return;
            if(_field.isFocused) return;

            if(evt.IsNumeric())
            {
                EnableManualInput();
            }

            else
            {
                HandleArrowInput(evt);
            }
        }
            
        #endregion

        
        #region Main

        public override void Increment(int increment)
        {
            BufferValue();
            CurrentValue += increment;
        }
        
        public override void Decrement(int decrement)
        {
            BufferValue();
            CurrentValue -= decrement;
        }

        /// <summary>
        /// Switch from standard keyboard input to numeric key input
        /// </summary>
        public void EnableManualInput()
        {
            var evt = Event.current;
            _isEditing = false;
            _field.ActivateInputField();
            _field.text = evt.GetNumber().ToString();
            _field.caretPosition = _field.selectionFocusPosition;
            evt.Use();
        }
            
        #endregion


        #region Plumbery

        private void HandleArrowInput(Event evt)
        {
            if (evt.keyCode == KeyCode.LeftArrow)
            {
                Decrement(1);
                Event.current.Use();
            }

            else if (evt.keyCode == KeyCode.RightArrow)
            {
                Increment(1);
;               Event.current.Use();
            }

            else if (evt.keyCode == KeyCode.UpArrow)
            {
                Increment(10);
                Event.current.Use();
            }

            else if (evt.keyCode == KeyCode.DownArrow)
            {
                Decrement(10);
                Event.current.Use();
            }
        }

        private void SetNumber(string number)
        {
            var value = 0;
            int.TryParse(number, out value);
            CurrentValue = value;
            OnSubmit(null);
        }

        #endregion        
    }
}