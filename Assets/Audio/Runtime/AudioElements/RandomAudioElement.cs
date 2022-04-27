using UnityEngine;

[System.Serializable]
public class RandomAudioElement : AudioElementBase, IMultiAudioElement
{
    [SerializeField] private AudioClip[] _clips;

    public AudioClip[] Clips => _clips;

    public override AudioClip GetClip()
    {
        if(_clips.Length == 0) throw new System.IndexOutOfRangeException("The clip list is empty, unable to get a random clip");
        
        var index = Random.Range(0, Clips.Length);
        return Clips[index];
    }

    public override void SetupSource(AudioSource source)
    {
        source.loop = false;
        source.clip = GetClip();
        source.volume = _volume;
    }
}