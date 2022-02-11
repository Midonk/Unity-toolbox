using UnityEngine;
using UnityEngine.UI;
using System;

namespace TF.DebugMenu.Components
{
    public class EnumHandle : Selectable
    {
        [SerializeField] private Text _enumUI;

        public int EnumValue
        {
            get => _enumValue;
            set
            {
                _enumValue = value;
                _enumUI.text = Enum.ToObject(_enumType, _enumValue).ToString();
            }
        }

        private void OnGUI() 
        {
            var evt = Event.current;
            if(currentSelectionState != SelectionState.Selected) return;
            if(!evt.isKey) return;
            if(evt.type != EventType.KeyDown) return;
            
            if(evt.keyCode == KeyCode.LeftArrow)
            {
                Decrement();
                Event.current.Use();
            }

            else if(evt.keyCode == KeyCode.RightArrow)
            {
                Increment();
                Event.current.Use();
            }
        }

        public void Decrement()
        {
            EnumValue = (int)Mathf.Repeat(EnumValue - 1, _enumLength);
        }
        
        public void Increment()
        {
            EnumValue = (int)Mathf.Repeat(EnumValue + 1, _enumLength);
        }

        public void SetType(Type enumType)
        {
            _enumType = enumType;
            _enumLength = Enum.GetNames(_enumType).Length;
        }

        private int _enumValue;
        private Type _enumType;
        private int _enumLength;
    }
}