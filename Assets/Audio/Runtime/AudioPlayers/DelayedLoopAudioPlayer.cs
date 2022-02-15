using UnityEngine;
using UnityEngine.Events;
using System.Collections;

// Allow an element to be played in a loop with a [random] delay [and pitch shift] between each loop

public class DelayedLoopAudioPlayer : AudioPlayer<SimpleAudioElement>, IAudioPlayer
{
    #region Exposed

    [SerializeField] private bool _randomDelay = true;
    [Tooltip("Delay will be determine between x and y seconds")]
    [SerializeField] private Vector2 _delayRange = new Vector2(0, 5);
    [SerializeField] private float _delay = 2;
    [SerializeField] private bool _randomPitch = false;
    [Tooltip("Random pitch will be determine between x ande y")]
    [SerializeField] private Vector2 _pitchRange = new Vector2(-0.25f, 0.25f);  
    [SerializeField] private UnityEvent _onBeforePlaySound;

    #endregion


    #region Main

    public override void Play()
    {
        _currentElement.SetupSource(_source);
        RestartCycle();            
    }

    public override void Stop() 
    {
        StopAllCoroutines();
        base.Stop();
    }

    private void RestartCycle()
    {
        StopAllCoroutines();
        var delay = _randomDelay ? Random.Range(_delayRange.x, _delayRange.y) : _delay;
        var pitch = _randomPitch ? 1 + Random.Range(_pitchRange.x, _pitchRange.y) : 1;

        _onBeforePlaySound?.Invoke();
        _source.pitch = pitch;
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