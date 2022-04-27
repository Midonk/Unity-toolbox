using TF.Utils;
using TF.DebugMenu.Attributes;

public class DebugAudioController : AudioMixerController
{
    [DebugMenuSlider("Options/Sound/General", 1, 0.0001f, 1)]
    public new void SetGeneralVolume(float volume)
    {
        AudioUtils.SetVolume(_mixer, _generalVolume, volume);
    }

    [DebugMenuSlider("Options/Sound/Music", 1, 0.0001f, 1)]
    public new void SetMusicVolume(float volume)
    {
        AudioUtils.SetVolume(_mixer, _musicVolume, volume);
    }
    
    [DebugMenuSlider("Options/Sound/Ambiances", 1, 0.0001f, 1)]
    public new void SetAmbianceVolume(float volume)
    {
        AudioUtils.SetVolume(_mixer, _ambianceVolume, volume);
    }
    
    [DebugMenuSlider("Options/Sound/SFX", 1, 0.0001f, 1)]
    public new void SetSFXVolume(float volume)
    {
        AudioUtils.SetVolume(_mixer, _sfxVolume, volume);
    }
    
    [DebugMenuSlider("Options/Sound/Voices", 1, 0.0001f, 1)]
    public new void SetVoicesVolume(float volume)
    {
        AudioUtils.SetVolume(_mixer, _voiceVolume, volume);
    }
}
