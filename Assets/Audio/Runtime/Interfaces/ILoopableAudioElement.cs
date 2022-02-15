using UnityEngine;

//Specific element template that allow to make a clip inloopout

public interface ILoopableAudioElement : IAudioElement
{
    LoopPhase Phase { get; }
    AudioClip ClipLoop { get; }
    AudioClip ClipIn { get; }
    AudioClip ClipOut { get; }
    AudioClip CurrentClip { get; }
}

public enum LoopPhase
{
    In,
    Loop,
    Out
}