using UnityEngine;

public class RandomAudioPlayer : MonoBehaviour
{
    [SerializeField]
    private AudioSource _source;

    [SerializeField]
    private AudioClip[] _clips;

    [SerializeField]
    [Range(0, 1)]
    private float _volume = 1;

    public void PlayRandomOneShot()
    {
        var index = Random.Range(0, _clips.Length);
        var clip = _clips[index];
        _source.volume = 1;
        _source.PlayOneShot(clip, _volume);
    }
    
    public void PlayRandom()
    {
        var index = Random.Range(0, _clips.Length);
        _source.clip = _clips[index];
        _source.volume = _volume;
        _source.Play();
    }
}