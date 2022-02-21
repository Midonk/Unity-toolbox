using UnityEngine;
using UnityEngine.Audio;

namespace TF.Utils
{
    public class AudioUtils : MonoBehaviour
    {
        /// <summary>
        /// Set the normalized linear volume of a given audio track (0.0001 - 1) to logarytmic dB (-80 - 0)
        /// </summary>
        /// <param name="mixer">Targetted mixer</param>
        /// <param name="mixerName">Targetted track (AudioMixerGroup)</param>
        /// <param name="volume">Normalized volume</param>
        public static void SetVolume(AudioMixer mixer, string mixerName, float volume)
        {
            volume = Mathf.Clamp(volume, 0.0001f, 1);
            volume = float2dB(volume);
            var mixerExists = mixer.SetFloat(mixerName, volume);
            if(mixerExists) return;
            
            Debug.LogWarning($"Mixer <color=orange>{mixerName}</color> doesn't exists, Unable to set its volume");        
        }

        /// <summary>
        /// Get the normalized linear volume of a given audio track (0.0001 - 1)
        /// </summary>
        /// <param name="mixer">Targetted mixer</param>
        /// <param name="mixerName">Targetted track (AudioMixerGroup)</param>
        /// <returns>Normalized volume</returns>
        public static float GetVolume(AudioMixer mixer, string mixerName)
        { 
            var mixerExists = mixer.GetFloat(mixerName, out float volume);
            if(!mixerExists)
            {
                Debug.LogWarning($"Mixer <color=orange>{mixerName}</color> doesn't exists, Unable to get its volume");
                return 0;
            }

            volume = dB2Float(volume);
            return volume;
        }

        public static string[] GetExposedParameters(AudioMixer mixer)
        {
            var mixerType = mixer.GetType();
            var mixerProperty = mixerType.GetProperty("exposedParameters");
            var parameters = (System.Array)mixerProperty.GetValue(mixer, null);
            var exposedParameters = new string[parameters.Length];
            for (int i = 0; i < parameters.Length; i++)
            {    
                var parameter = parameters.GetValue(i);
                var parameterType = parameter.GetType();
                var parameterField = parameterType.GetField("name");
                var parameterName = (string)parameterField.GetValue(parameter);
                exposedParameters[i] = parameterName;
            }

            return exposedParameters;
        }

        public static string[] GetTrackNames(AudioMixer mixer)
        {
            var tracks = mixer.FindMatchingGroups(string.Empty);
            var trackNames = new string[tracks.Length];
            for (int i = 0; i < tracks.Length; i++)
            {
                trackNames[i] = tracks[i].name;
            }

            return trackNames;
        }

        /// <summary>
        /// Convert linear volume to decibel
        /// </summary>
        /// <param name="value">Linear volume</param>
        /// <returns>volume in decibel</returns>
        public static float float2dB(float value)
        {
            return Mathf.Log10(value) * 20f;
        }

        /// <summary>
        /// Convert decibel to linear volume
        /// </summary>
        /// <param name="value">decibel</param>
        /// <returns>linear volume</returns>
        public static float dB2Float(float value)
        {
            value /= 20f;
            return Mathf.Pow(10, value);
        }
    }
}
