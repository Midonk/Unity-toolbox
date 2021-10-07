using UnityEngine;

public static class AudioSourceExtension
{    
    public static void Play(this AudioSource source, AudioClip clip)
    {
        source.clip = clip;
        source.Play();
    }
    
    /* public static void PlayChain(this AudioSource source, AudioClip clip)
    {
        var time = source.time;
        
    }
    
    public static void Stop(this AudioSource source)
    {
        source.time = 0;
        source.Stop();
        Debug.Log("hey");
    } */
}