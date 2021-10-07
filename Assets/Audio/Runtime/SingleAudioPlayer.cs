using UnityEngine;

public class SingleAudioPlayer : MonoBehaviour
{
    [SerializeField]
    private AudioSource _source;

    [SerializeField][Range(0, 1)]
    private float _volume = 1;

    [SerializeField]
    private AudioClip _clip;

    public void PlayClip()
    {
        if(!_clip) return;
        
        _source.loop = false;
        _source.volume = _volume;
        PlayClip(_clip);
    }
    
    public void PlayClip(AudioClip clip)
    {
        _source.clip = clip;
        _source.loop = false;
        _source.volume = _volume;
        _source.Play();
    }
    
    public void PlayClipOneShot()
    {
        if(!_clip) return;

        _source.volume = 1;
        _source.PlayOneShot(_clip, _volume);
    }
    
    public void PlayClipOneShot(AudioClip clip)
    {
        _source.volume = 1;
        _source.PlayOneShot(clip, _volume);
    }

    public void Stop()
    {
        _source.Stop();
    }
}
