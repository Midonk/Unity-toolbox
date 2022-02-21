using UnityEngine;
using UnityEngine.Audio;
using TF.Utils;

public class AudioMixerController : MonoBehaviour
{
    [SerializeField] protected AudioMixer _mixer;
    [SerializeField] protected string _generalVolume;
    [SerializeField] protected string _musicVolume;
    [SerializeField] protected string _ambianceVolume;
    [SerializeField] protected string _sfxVolume;
    [SerializeField] protected string _voiceVolume;

    public void SetGeneralVolume(float volume)
    {
        AudioUtils.SetVolume(_mixer, _generalVolume, volume);
    }

    public void SetMusicVolume(float volume)
    {
        AudioUtils.SetVolume(_mixer, _musicVolume, volume);
    }
    
    public void SetAmbianceVolume(float volume)
    {
        AudioUtils.SetVolume(_mixer, _ambianceVolume, volume);
    }
    
    public void SetSFXVolume(float volume)
    {
        AudioUtils.SetVolume(_mixer, _sfxVolume, volume);
    }
    
    public void SetVoicesVolume(float volume)
    {
        AudioUtils.SetVolume(_mixer, _voiceVolume, volume);
    }
}