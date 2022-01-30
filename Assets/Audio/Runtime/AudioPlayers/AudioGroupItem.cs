using UnityEngine;
 
//Manipulate audio elements, serve them as place to live in

public abstract class AudioGroupItem : MonoBehaviour, IAudioGroupItem
{
    #region Exposed

    [SerializeField] protected AudioGroup[] _groups;

    #endregion


    #region Properties

    public IAudioGroup<IAudioGroupItem>[] Groups => _groups;

    #endregion


    #region Unity API

    protected void OnEnable() 
    { 
        RegisterToGroups();
    }

    protected void OnDisable() 
    {
        UnregisterToGroups();
    }
        
    #endregion


    #region Utils

    public void RegisterToGroups()
    {
        for (int i = 0; i < _groups.Length; i++)
        {
            _groups[i].Add(this);
        }
    }

    public void UnregisterToGroups()
    {
        for (int i = 0; i < _groups.Length; i++)
        {
            _groups[i].Remove(this);
        }
    }

    #endregion
}