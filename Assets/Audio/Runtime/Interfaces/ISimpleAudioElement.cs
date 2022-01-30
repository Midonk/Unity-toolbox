using UnityEngine;

//Allow a player to play a simple clip

public interface ISimpleAudioElement : IAudioElement
{
    AudioClip Clip { get; }
}