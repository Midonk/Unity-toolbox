using UnityEngine;
using UnityEngine.Events;

public class InputSequencer : MonoBehaviour
{
    #region Exposed

    [SerializeField]
    private KeyCode[] _sequence;

    [SerializeField][Min(0)]
    private float _maxInputSpacingTime;

    [SerializeField]
    private UnityEvent _onValidation;

    #endregion

    
    #region Unity API

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
        //Debug.Log($"validated {_progression} / {_sequence.Length}");
        if (_progression < _sequence.Length) return;
        
        //Debug.Log("Sequence validated");
        _onValidation.Invoke();
        ResetProgress();
    }

    #endregion


    #region Utils

    private void ResetProgress()
    {
        if(_progression == 0) return;

        //Debug.Log("Reset sequence");
        _progression = 0;
    }
        
    #endregion


    #region Private Fields

    private float _lastInputTime;
    private int _progression;

    #endregion
}