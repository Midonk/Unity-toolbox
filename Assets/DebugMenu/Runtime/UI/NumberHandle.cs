using System;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class NumberHandle : InputField
{
    public int Number
    {
        get => _number;
        set
        {
            _number = value;
            text = _number.ToString();
        }
    }

    protected override void Awake() 
    {
        base.Awake();
        onEndEdit.AddListener(SetNumber);
    }

    private void OnGUI()
    {
        var evt = Event.current;
        if (currentSelectionState != SelectionState.Selected) return;
        if (evt.type != EventType.KeyDown) return;
        if(isFocused) return;

        if(evt.numeric)
        {
            text = evt.character.ToString();
            ActivateInputField();
        }

        else
        {
            HandleArrowInput(evt);
        }
    }

    private void HandleArrowInput(Event evt)
    {
        if (evt.keyCode == KeyCode.LeftArrow)
        {
            Number--;
            Event.current.Use();
        }

        else if (evt.keyCode == KeyCode.RightArrow)
        {
            Number++;
            Event.current.Use();
        }

        else if (evt.keyCode == KeyCode.UpArrow)
        {
            Number += 10;
            Event.current.Use();
        }

        else if (evt.keyCode == KeyCode.DownArrow)
        {
            Number -= 10;
            Event.current.Use();
        }
    }

    public override void OnSubmit(BaseEventData eventData)
    {
        onEndEdit?.Invoke(text);
    }

    public void Increment(int increment)
    {
        Number += increment;
    }
    
    public void Decrement(int decrement)
    {
        Number -= decrement;
    }

    public void SetNumber(string number)
    {
        var value = 0;
        int.TryParse(number, out value);
        Number = value;
    }

    private int _number;
}