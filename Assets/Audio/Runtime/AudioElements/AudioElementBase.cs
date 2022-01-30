using UnityEngine;

// An AudioElement is a piece of logic handling sound(s) behaviour. 
// ???[Two AudioElements of the same type may differ in some aspect like the way the play or stop via outter logic]??? maybe not

[System.Serializable]
public abstract class AudioElementBase : IAudioElement
{
    [SerializeField] protected string _id;
    [Range(0, 1)]
    [SerializeField] protected float _volume = 1;

    public string Id => _id;
    public float Volume => _volume;

    public abstract AudioClip GetClip();
    public abstract void SetupSource(AudioSource source);
}