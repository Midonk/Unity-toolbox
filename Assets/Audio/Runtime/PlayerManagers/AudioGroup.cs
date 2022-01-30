using UnityEngine;
using Utils;

// An AudioGroup is an handle allowing to manage a list of AudioPlayers with a single reference.

/*
* Create custom editor to display registered players at runtime
*/

[CreateAssetMenu(menuName = "Audio/Groups/Group", fileName = "newAudioGroup")]
public class AudioGroup : ScriptableObject, IAudioGroup<IAudioGroupItem>
{
    public SmartList<IAudioGroupItem> Players => _players;

    public void Add(IAudioGroupItem player)
    {        
        _players.Add(player);
    }

    public void Remove(IAudioGroupItem player)
    {        
        _players.Remove(player);
    }

    [SerializeField] private SmartList<IAudioGroupItem> _players;
}