using UnityEngine;

// An AudioPlayer is an handle that register a list of AudioElements of the same type (sharing a common logic)

public abstract class AudioPlayer<T> : AudioGroupItem, IAudioPlayer where T : IAudioElement
{
    #region Exposed

    [SerializeField] protected AudioSource _source;
    [SerializeField] protected T[] _audioElements;
        
    #endregion


    #region Properties

    public AudioSource Source => _source;

    #endregion


    #region Main

    public void Play(AudioClip clip)
    {
        Play(clip.name);
    }

    public void Play(string elementId)
    {
        var element = GetElement(elementId);
        Play(element);
    }

    public void Play(IAudioElement element)
    {
        _currentElement = element;
        Play();
    }
    
    public abstract void Play();

    public void PlayOneShot(AudioClip clip)
    {
        PlayOneShot(clip.name);
    }

    public void PlayOneShot(string elementId)
    {
        var element = GetElement(elementId);
        PlayOneShot(element);
    }

    public virtual void PlayOneShot(IAudioElement element)
    {
        var volume = Mathf.Min(element.Volume, 1);
        var clip = element.GetClip();
        _source.PlayOneShot(clip, volume);
    }

    public virtual void Pause()
	{
		_source.Pause();
	}

	public virtual void Unpause()
	{
		_source.UnPause();
	}

	public virtual void Stop()
	{
		_source.Stop();
	}
         
    #endregion
    

    #region Utils

    /// <summary>
    ///     Get an AudioElement by its ID
    /// </summary>
    /// <param name="id"></param>
    /// <returns></returns>
    public IAudioElement GetElement(string id)
    {
        for (int i = 0; i < _audioElements.Length; i++)
        {
            var element = _audioElements[i];
            if(element.Id.Equals(id))
            {
                return element;
            }
        }

        throw new System.ArgumentException($"ID '<color=cyan>{id}</color>' not found in the <color=cyan>{name}'s ({this.GetType()})</color> AudioPlayer");
    } 

    #endregion


    #region Private Fields

    protected IAudioElement _currentElement;
         
    #endregion 
}