using UnityEngine;
using System.Collections.Generic;

[CreateAssetMenu(menuName = "Audio/Group", fileName = "newAudioGroup")]
public class AudioGroup : ScriptableObject
{
    internal void Add(AudioPlayer player)
    {
        if (_players.Contains(player)) return;
        
        _players.Add(player);
    }

    internal void Remove(AudioPlayer player)
    {
        if (!_players.Contains(player)) return;
        
        _players.Remove(player);
    }

    [SerializeField]
    private List<AudioPlayer> _players;
}