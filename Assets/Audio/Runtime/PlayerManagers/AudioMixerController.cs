using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Audio;

public class AudioMixerController : MonoBehaviour
{
    [SerializeField] private AudioMixer _mainMixer;

    private void Awake()
    {
        if(!_mainMixer)
        {
            throw new System.NullReferenceException("Missing AudioMixer reference on the AudioController");
        }
    }

    public void SetVolume(string mixerName, float volume)
    {
        volume = Mathf.Clamp(volume, 0.0001f, 1);
        volume = float2dB(volume);
        var mixerExists = _mainMixer.SetFloat(mixerName, volume);
        if(mixerExists) return;
        
        Debug.LogWarning($"Mixer <color=orange>{mixerName}</color> doesn't exists, Unable to set its volume", this);        
    }

    public float GetVolume(string mixerName)
    { 
        var mixerExists = _mainMixer.GetFloat(mixerName, out float volume);
        if(!mixerExists)
        {
            Debug.LogWarning($"Mixer <color=orange>{mixerName}</color> doesn't exists, Unable to get its volume", this);
            return 0;
        }

        volume = dB2Float(volume);
        return volume;
    }

    public static float float2dB(float value)
    {
        return Mathf.Log10(value) * 20f;
    }

    public static float dB2Float(float value)
    {
        value /= 20f;
        return Mathf.Pow(10, value);
    }
}