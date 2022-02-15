using UnityEngine;
using UnityEngine.UI;
using System;

namespace TF.DebugMenu.Components
{
    public class EnumHandle : UIHandle<int>
    {   
        #region Exposed

        [SerializeField] private Text _enumUI;

        #endregion

        
        #region Properties

        public override int CurrentValue
        {
            get => _currentValue;
            set
            {
                _currentValue = value;
                _enumUI.text = Enum.ToObject(_enumType, _currentValue).ToString();
            }
        }

        #endregion

        
        #region Unity API

        private void OnGUI()
        {
            var evt = Event.current;
            if (currentSelectionState != SelectionState.Selected) return;
            if (!evt.isKey) return;
            if (evt.type != EventType.KeyDown) return;

            if (evt.keyCode == KeyCode.LeftArrow)
            {
                Decrement();
                Event.current.Use();
            }

            else if (evt.keyCode == KeyCode.RightArrow)
            {
                Increment();
                Event.current.Use();
            }
        }

        #endregion

        
        #region Main

        public override void Decrement(int step = 1)
        {
            BufferValue();
            CurrentValue = (int)Mathf.Repeat(CurrentValue - 1, _enumLength);
        }

        public override void Increment(int step = 1)
        {
            BufferValue();
            CurrentValue = (int)Mathf.Repeat(CurrentValue + 1, _enumLength);
        }
            
        #endregion

        
        #region Utils

        public void SetType(Type enumType)
        {
            _enumType = enumType;
            _enumLength = Enum.GetNames(_enumType).Length;
        }

        #endregion

        
        #region Private Fields

        private Type _enumType;
        private int _enumLength;

        #endregion
    }
}