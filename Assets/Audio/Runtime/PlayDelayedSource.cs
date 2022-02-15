using System.Collections;
using UnityEngine;

public class PlayDelayedSource : MonoBehaviour
{
    /* public AudioSource _source;
    public AudioClip _clip1;
    public AudioClip _clip2;


    private void OnGUI() 
    {
        if(GUILayout.Button("play"))
        {
            StartCoroutine(PlayFollow());
        }    

        if(GUILayout.Button("play delayed"))
        {
            PlayDelayed();
        }

        if(GUILayout.Button("Stop"))
        {
            Stop();
        }
    }


    private void Play()
    {
        _source.Play(_clip1);
    }

    private void Stop()
    {
        _source.Stop();
    }

    private void PlayDelayed()
    {
        _source.clip = _clip2;

    }

    private IEnumerator PlayFollow()
    {
        _source.Play(_clip1);
        yield return new WaitForSeconds(_clip1.length);
        _source.Play(_clip2);
    } */
}