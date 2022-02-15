using UnityEngine;

//Base audio player that handles Audio elements

public interface IAudioPlayer : IAudioGroupItem
{
    AudioSource Source { get; }

    void Play();
    void PlayOneShot(IAudioElement element);
    void Pause();
    void Unpause();
    void Stop();
    IAudioElement GetElement(string id);
}