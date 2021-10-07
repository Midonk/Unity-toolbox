using UnityEngine;

public class MultiRandomAudioPlayer : MonoBehaviour
{
    [SerializeField]
    private AudioSource _source;

    [SerializeField]
    private AudioGroup[] _audioGroups;

    public void PlayRandomOneShot(string groupName)
    {
        var group = GetAudioGroup(groupName);
        var index = Random.Range(0, group.clips.Length);
        var clip = group.clips[index];
        
        _source.PlayOneShot(clip, group.volume);
    }
    
    public void PlayRandom(string groupName)
    {
        var group = GetAudioGroup(groupName);
        if(group == null) return;

        var index = Random.Range(0, group.clips.Length);
        var clip = group.clips[index];

        _source.clip = clip;
        _source.volume = group.volume;
        _source.Play();
    }

    private AudioGroup GetAudioGroup(string groupName)
    {
        foreach (var group in _audioGroups)
        {
            if(!group.groupName.Equals(groupName)) continue;
            
            if(group.clips.Length == 0)
            {
                Debug.LogError($"The requested audio group '{groupName}' doesn't have any clip referenced", this);
                return null;
            }

            return group;
        }
        
        Debug.LogError($"The requested audio group '{groupName}' doesn't exists", this);
        return null;
    }

    [System.Serializable]
    private class AudioGroup
    {
        public string groupName;
        [Range(0, 1)]
        public float volume = 0;
        public AudioClip[] clips;
    }
}