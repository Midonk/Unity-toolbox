
public class SimpleAudioPlayer : AudioPlayer<SimpleAudioElement>, IAudioPlayer
{
    public override void Play()
    {
        _currentElement?.SetupSource(_source);
        _source.Play();
    }
}