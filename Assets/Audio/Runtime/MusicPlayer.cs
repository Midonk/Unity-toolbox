using UnityEngine;

public class MusicPlayer : MonoBehaviour
{
    [SerializeField]
    private AudioSource _source;
    
    [SerializeField]
    private float _volume;

    public void PlayChain(AudioClip clip)
    {
        var time = _source.time;
        _source.clip = clip;
        _source.Play();
        _source.time = time;
    }

    public void PlayClip(AudioClip clip)
    {
        _source.clip = clip;
        _source.volume = _volume;
        _source.Play();
    }
}