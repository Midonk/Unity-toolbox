
public class RandomAudioPlayer : AudioPlayer<RandomAudiElement>, IAudioPlayer
{
    public override void Play()
    {
        _currentElement?.SetupSource(_source);
        _source.Play();
    }
}