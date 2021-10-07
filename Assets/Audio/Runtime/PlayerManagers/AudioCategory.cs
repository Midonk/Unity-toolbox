using UnityEngine;
using System.Collections.Generic;

[CreateAssetMenu(menuName = "Audio/Category", fileName = "newAudioCategory")]
public class AudioCategory : ScriptableObject
{
    public void Pause()
    {
        var pausedPlayer = new List<AudioPlayer>();
        foreach (var player in _players)
        {
            if(player.Paused) continue;

            player.Pause();
            pausedPlayer.Add(player);
        }

        _pausedPlayers = pausedPlayer.ToArray();
    }

    public void Stop()
    {
        foreach (var source in _players)
        {
            source.Stop();
        }

        _players.Clear();
    }

    public void Unpause()
    {
        foreach (var player in _pausedPlayers)
        {
            player.UnPausePlayer();
        }
    }

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
    private AudioPlayer[] _pausedPlayers;
}