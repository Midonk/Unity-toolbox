using UnityEngine;
using UnityEngine.Events;

public class InputSequencer : MonoBehaviour
{
    #region Exposed

    [SerializeField] private KeyCode[] _sequence;
    [Min(0)]
    [SerializeField] private float _maxInputSpacingTime;
    [SerializeField] private UnityEvent _onValidation;

    #endregion

    
    #region Unity API

    //may do something less overkill?
    void Update()
    {
        RefreshSequence();
    }
        
    #endregion


    #region Main

    private void RefreshSequence()
    {
        if(_sequence.Length == 0) return;

        var canEnterNextInput = Time.time <= _lastInputTime + _maxInputSpacingTime || _progression == 0 && _progression < _sequence.Length;
        if (canEnterNextInput)
        {
            if (!Input.anyKeyDown) return;
            
            var nextInputToPress = _sequence[_progression];
            if (!Input.GetKeyDown(nextInputToPress))
            {
                ResetProgress();
                return;
            }
            
            Progress();
        }

        else
        {
            ResetProgress();
        }
    }

    private void Progress()
    {
        _lastInputTime = Time.time;
        _progression++;
        if (_progression < _sequence.Length) return;
        
        _onValidation.Invoke();
        ResetProgress();
    }

    #endregion


    #region Plumbery

    private void ResetProgress()
    {
        if(_progression == 0) return;

        _progression = 0;
    }
        
    #endregion


    #region Private Fields

    private float _lastInputTime;
    private int _progression;

    #endregion
}