using UnityEngine;
using Utils;

//Allow to controle an entire sound category with a single reference

[CreateAssetMenu(menuName = "Audio/Groups/Category", fileName = "newAudioCategory")]
public class AudioCategory : ScriptableObject, IAudioGroup<IAudioPlayer>
{
    public SmartList<IAudioPlayer> Players { get; private set; } = new SmartList<IAudioPlayer>();

    public void Add(IAudioPlayer audioPlayer)
    {
        Players.Add(audioPlayer);
    }
    
    public void Remove(IAudioPlayer audioPlayer)
    {
        throw new System.NotImplementedException();
    }

    public void Pause()
    {
        foreach (IAudioPlayer player in Players)
        {
            player.Pause();
        }
    }


    public void Stop()
    {
        foreach (IAudioPlayer source in Players)
        {
            source.Stop();
        }

        Players.Clear();
    }

    public void Unpause()
    {
        foreach (IAudioPlayer player in Players)
        {
            player.Unpause();
        }
    }
}