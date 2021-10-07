using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MultiAudioController : AudioPlayer
{
    #region Exposed

    [SerializeField]
    private AudioSource[] _sources;

    [SerializeField]
    private bool _muteOnStart = true;

    [Header("Debug")]
    [SerializeField]
    private bool _debugMode;
         
    #endregion


    #region Unity API

    protected override void Awake() 
    {
        base.Awake();
        foreach (var source in _sources)
        {
            _maxVolumeReference.Add(source, source.volume);
        }    
    }

    private void Start() 
    {
        foreach (var source in _sources)
        {
            if(_muteOnStart) source.volume = 0;
        }
    }

    #endregion


    
    #region Main

    public void PlaySources()
    {
        foreach (var source in _sources)
        {
            if(source.isPlaying) continue;
            source.Play();
        }
    }

    public void StopSources()
    {
        foreach (var source in _sources)
        {
            if(!source.isPlaying) continue;
            source.Stop();
            source.time = 0;
        }
    }

    public void PlayFadeSources(float fadeDuration)
    {
        StopAllCoroutines();
        StartCoroutine(FadeInSources(fadeDuration));
    }
    
    public void StopFadeSources(float fadeDuration)
    {
        StopAllCoroutines();
        StartCoroutine(FadeOutSources(fadeDuration));
    }

    public override void Pause()
    {
        foreach (var source in _sources)
        {
            if(!source.isPlaying) continue;
            source.Pause();
        }
    }

    public override void Unpause()
    {
        foreach (var source in _sources)
        {
            source.UnPause();
        }
    }

    public override void Stop()
    {
        StopSources();
    }
         
    #endregion


    #region Coroutines
        
    private IEnumerator FadeOutSources(float fadeDuration)
    {
        int sourceToFade = _sources.Length;
        while (sourceToFade > 0)
        {
            foreach (var source in _sources)
            {
                var maxVolume = _maxVolumeReference[source];
                var volumeDecrement = maxVolume / fadeDuration * Time.deltaTime;
                if(source.volume <= volumeDecrement)
                {
                   sourceToFade --;
                   source.volume = 0;
                }

                else
                {
                   source.volume -= volumeDecrement;
                }
            }

            yield return null;
        }

        StopSources();
    }
    
    private IEnumerator FadeInSources(float fadeDuration)
    {
        int sourceToFade = _sources.Length;
        PlaySources();
        while (sourceToFade > 0)
        {
            foreach (var source in _sources)
            {
                var maxVolume = _maxVolumeReference[source];
                var volumeIncrement = maxVolume / fadeDuration * Time.deltaTime;
                if(maxVolume - source.volume <= volumeIncrement)
                {
                   sourceToFade --;
                   source.volume = maxVolume;
                }

                else
                {
                   source.volume += volumeIncrement;
                }
            }

            yield return null;
        }
    }

    #endregion


    #region Private Fields

    private Dictionary<AudioSource, float> _maxVolumeReference = new Dictionary<AudioSource, float>();
         
    #endregion
}
