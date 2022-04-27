
public class RandomAudioPlayer : AudioPlayer<RandomAudioElement>, IAudioPlayer
{
    public override void Play()
    {
        _currentElement?.SetupSource(_source);
        _source.Play();
    }
}