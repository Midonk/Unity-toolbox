using UnityEngine;

// Element type handled by an audio player

public interface IAudioElement
{
    string Id { get; }
    float Volume { get; }

    void SetupSource(AudioSource source);
    AudioClip GetClip();
}