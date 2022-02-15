using UnityEngine;

//Allow a player to play a clip inside a list. It depends of what the implementation of the element wants to do with this list

public interface IMultiAudioElement : IAudioElement
{
    AudioClip[] Clips { get; }
}