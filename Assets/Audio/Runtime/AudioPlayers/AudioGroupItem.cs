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

    protected virtual void OnEnable() 
    { 
        RegisterToGroups();
    }

    protected virtual void OnDisable() 
    {
        UnregisterToGroups();
    }
        
    #endregion


    #region Plumbery

    private void RegisterToGroups()
    {
        for (int i = 0; i < _groups.Length; i++)
        {
            RegisterToGroup(_groups[i]);
        }
    }

    private void UnregisterToGroups()
    {
        for (int i = 0; i < _groups.Length; i++)
        {
            UnregisterToGroup(_groups[i]);
        }
    }
        
    #endregion


    #region Utils

    public void RegisterToGroup(IAudioGroup<IAudioGroupItem> group)
    {
        group.Add(this);
    }

    public void UnregisterToGroup(IAudioGroup<IAudioGroupItem> group)
    {
        group.Remove(this);
    }

    #endregion
}