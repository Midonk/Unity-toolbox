

using UnityEngine;

[System.Serializable]
public class SimpleAudioElement : AudioElementBase, ISimpleAudioElement
{
    [SerializeField] private AudioClip _clip;

    public AudioClip Clip => _clip;

    public override AudioClip GetClip() => Clip;

    public override void SetupSource(AudioSource source)
    {
        source.loop = false;
        source.clip = _clip;
        source.volume = _volume;
    }
}