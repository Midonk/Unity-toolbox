using UnityEngine;
using UnityEngine.Events;
using System.Collections;

namespace SoundRuntime
{

    public class RandomAmbiancePlayer : MonoBehaviour
    {
        #region Exposed

        [SerializeField]
        private AudioSource _source;

        [SerializeField]
        private Vector2 _delayRange = new Vector2(0, 5);

        [SerializeField]
        private bool _randomPitch = false;
        
        [SerializeField]
        private UnityEvent _onBeforePlaySound;


        [Header("Debug")]
        [SerializeField]
        private bool _debugMode;

        #endregion


        private void OnGUI() 
        {
            if(!_debugMode) return;

            if(GUILayout.Button("Start cycle"))
            {
                StartCycle();
            }
            
            if(GUILayout.Button("Stop cycle"))
            {
                StopCycle();
            }
        }


        #region Main

        public void StartCycle() 
        {
            RestartCycle();
        }

        public void StopCycle() 
        {
            StopAllCoroutines();
        }

        private void RestartCycle()
        {
            StopAllCoroutines();
            var delay = Random.Range(_delayRange.x, _delayRange.y);
            if(_randomPitch)
            {
                var randomPitch = Random.Range(-0.25f, 0.25f);
                _source.pitch = 1 + randomPitch;
            }

            _onBeforePlaySound?.Invoke();
            StartCoroutine(PlayDelay(delay));
        }
            
        #endregion


        #region Coroutine

        private IEnumerator PlayDelay(float delay)
        {
            if(!_source.isPlaying)
            {
                _source.Play();
            }

            while (_source.isPlaying)
            {
                yield return null;
            }

            yield return new WaitForSeconds(delay);
            RestartCycle();
        }
            
        #endregion
    }
}
