using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Audio;

namespace AudioRuntime
{
    public class AudioMixerController : MonoBehaviour
    {
        [SerializeField]
        private AudioMixer _mainMixer;

        private void Awake()
        {
            if(!_mainMixer)
            {
                throw new System.NullReferenceException("Missing AudioMixer reference on the AudioController");
            }

            _mixers = new List<string>();
            var mixers = _mainMixer.FindMatchingGroups("");
            foreach (var mixer in mixers)
            {
                _mixers.Add(mixer.name);
            }
        }

        public string[] Mixers => _mixers.ToArray();

        public void SetVolume(string mixerName, float volume)
        {
            volume = Mathf.Clamp(volume, 0.0001f, 1);
            volume = float2dB(volume);
            _mainMixer.SetFloat($"{mixerName}Volume", volume);
        }

        public float GetVolume(string mixerName)
        { 
            _mainMixer.GetFloat($"{mixerName}Volume", out float volume);
            volume = dB2Float(volume);

            return volume;
        }

        public static float float2dB(float value)
        {
            return Mathf.Log10(value) * 20f;
        }

        public static float dB2Float(float value)
        {
            value /= 20f;

            return Mathf.Pow(10, value);
        }

        private List<string> _mixers;
    }
}