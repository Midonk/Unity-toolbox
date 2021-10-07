using UnityEngine;
using System.Collections;

namespace ToolBox
{
	public class EntityAudioPlayer : MonoBehaviour
	{
		#region Exposed

		[SerializeField]
		private AudioSource _source;

		[SerializeField]
		private AudioLoopable[] _audioElements;
			 
		#endregion


		#region Main

		public void PlayElement(string elementName)
		{
			var element = GetElement(elementName);
			if(element == null) return;

			ChangeAudio(element, element.volume);
		}

		public void PlayElementOneShot(string elementName)
		{
			var element = GetElement(elementName);
			if(element == null) return;
			
			PlayOneShot(element.clip, element.volume);		
		}

		public void PlayLoopOut(bool oneShot = false)
		{
			if(_currentElement != null && _currentElement.inLoopOut && _currentElement.clipOut)
			{
				if(oneShot)
				{
					PlayOneShot(_currentElement.clipOut, _currentElement.volume);
				}

				else
				{
					PlaySingle(_currentElement.clipOut, _currentElement.volume);
				}

				return;
			}

			Debug.LogWarning($"Unable to play the loop-out of the current element, maybe you forgot to provide an out clip or the element isn't in loop mode", this);
		}

        private void ChangeAudio(AudioLoopable element, float volume)
        {
			_currentElement = element;
            if(element.inLoopOut && element.clipIn)
			{
				PlayInLoop(element);
			}

			else if(element.inLoopOut)
			{
				PlayLoop(element.clip, volume);
			}

			else
			{
				PlaySingle(element.clip, volume);
			}
        }

        private void PlayInLoop(AudioLoopable element)
        {
            PlaySingle(element.clipIn, element.volume);
			StartCoroutine(TrackEndOFAudio());
        }

		private void PlayLoop(AudioClip clip, float volume)
		{
			_source.loop = true;
			_source.volume = volume;
			_source.clip = clip;
			_source.Play();
			StopAllCoroutines();
		}

		private void PlaySingle(AudioClip clip, float volume)
		{
			_source.loop = false;
			_source.volume = volume;
			_source.clip = clip;
			_source.Play();
			StopAllCoroutines();
		}
		
		private void PlayOneShot(AudioClip clip, float volume)
		{
			StopAllCoroutines();
			_source.PlayOneShot(clip, volume);
		}

		public void Stop()
		{
			_source.Stop();
		}

		#endregion



		private AudioLoopable GetElement(string elementName)
		{
			foreach (var element in _audioElements)
			{
				if(element.name.Equals(elementName))
				{
					return element;
				}
			}

			Debug.LogWarning($"'{elementName}' not found in the 'EntityAudioModule' module", this);
			return null;
		}


        private IEnumerator TrackEndOFAudio()
        {
            while (_source.isPlaying)
			{
				 yield return null;
			}

			PlayLoop(_currentElement.clip, _currentElement.volume);
        }

		private AudioLoopable _currentElement;
	}
	
	
}
