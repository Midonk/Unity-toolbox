using UnityEngine;

public abstract class AudioPlayer : MonoBehaviour
{
    #region Exposed

    [SerializeField]
    private AudioCategory _category;
    
    [SerializeField]
    private AudioGroup _group;
         
    #endregion

    
    #region Properties

    public bool Paused => _paused;

    #endregion


    #region Unity API

    protected virtual void Awake() 
    {
        if(_category)
        {
            _category.Add(this);
        }
        
        if(_group)
        {
            _group.Add(this);
        }
    }

    private void OnDisable() 
    {
        if(_category)
        {
            _category.Remove(this); 
        }
        
        if(_group)
        {
            _group.Remove(this); 
        }
    }
         
    #endregion

    internal void PausePlayer()
    {
        Pause();
        _paused = true;
    }
    
    internal void UnPausePlayer()
    {
        Unpause();
        _paused = false;
    }

    internal void StopPlayer()
    {
        Stop();
        _paused = false;
    }

    public abstract void Pause();
    public abstract void Unpause();
    public abstract void Stop();

    private bool _paused;
}