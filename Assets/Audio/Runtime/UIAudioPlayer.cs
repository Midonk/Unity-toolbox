using UnityEngine;

public class UIAudioPlayer : MonoBehaviour
{
    #region Exposed

    [SerializeField]
    private AudioSource _source;

    [SerializeField]
    private AudioClip _pressed;
    
    [SerializeField]
    private AudioClip _selected;
    
    [SerializeField]
    private AudioClip _back;
         
    #endregion


    #region Main
         
    public void PlayPressed()
    {
        _source.PlayOneShot(_pressed);
    }
    
    public void PlaySelected()
    {
        _source.PlayOneShot(_selected);
    }
    
    public void PlayBack()
    {
        _source.PlayOneShot(_back);
    }

    #endregion
}