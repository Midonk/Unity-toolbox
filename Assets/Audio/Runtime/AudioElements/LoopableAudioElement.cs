using UnityEngine;

[System.Serializable]
public class LoopableAudioElement : AudioElementBase, ILoopableAudioElement
{
    [SerializeField] private AudioClip _clipIn;
    [SerializeField] private AudioClip _clipLoop;
    [SerializeField] private AudioClip _clipOut;

    public AudioClip ClipIn => _clipIn;
    public AudioClip ClipLoop => _clipLoop;
    public AudioClip ClipOut => _clipOut;
    public AudioClip CurrentClip => _currentClip;
    public LoopPhase Phase => _phase;


    public override AudioClip GetClip()
    {
        switch (_phase)
        {
            case LoopPhase.In: return _clipIn;
            case LoopPhase.Loop: return _clipLoop;
            case LoopPhase.Out: return _clipOut;
            default: return _clipLoop;
        }
    }
    
    private AudioClip NextClip
    {
        get 
        {
            switch (_phase)
            {
                case LoopPhase.In: return _clipLoop;
                case LoopPhase.Loop: return _clipOut;
                default: return _clipIn ?? _clipLoop;
            }
        }
    }

    public override void SetupSource(AudioSource source)
    {
		source.clip = NextClip;
        source.volume = _volume;
        _phase = _phase == LoopPhase.Out ? LoopPhase.In : _phase + 1;
        source.loop = _phase == LoopPhase.Loop;
    }

    private AudioClip _currentClip;
    private LoopPhase _phase = LoopPhase.Out;
}