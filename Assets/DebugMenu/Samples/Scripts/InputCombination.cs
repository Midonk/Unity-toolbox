using UnityEngine;
using UnityEngine.Events;

namespace DebugMenu
{
    public class InputCombination : MonoBehaviour
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

            if (Time.time <= _lastInputTime + _maxInputSpacingTime || _progression == 0 && _progression < _sequence.Length)
            {
                var nextInputToPress = _sequence[_progression];
                if (!Input.anyKeyDown) return;
                if (!Input.GetKeyDown(nextInputToPress))
                {
                    ResetProgress();
                    return;
                }
                
                _lastInputTime = Time.time;
                _progression++;
                Debug.Log($"validated {_progression} / {_sequence.Length}");
                if (_progression < _sequence.Length) return;
                
                Debug.Log("Sequence validated");
                _onValidation.Invoke();
                ResetProgress();
            }

            else
            {
                ResetProgress();
            }
        }
            
        #endregion


        #region Utils

        private void ResetProgress()
        {
            if(_progression == 0) return;

            Debug.Log("Reset sequence");
            _progression = 0;
        }
            
        #endregion


        #region Private Fields

        private float _lastInputTime;
        private int _progression;

        #endregion
    }
}