using UnityEngine;
using System.Collections;

namespace ToolBox
{
	public class LoopAudioPlayer : AudioPlayer
	{
		#region Exposed

		[SerializeField]
		private AudioSource _source;

		[SerializeField]
		private AudioLoopable[] _audioLoops;
			 
		#endregion


		#region Main

		public void Play(string elementName)
		{
			var element = GetElement(elementName);
			ChangeAudio(element);
		}

		public void PlayLoopOut(bool oneShot = false)
		{
			if(_currentLoop != null && _currentLoop.inLoopOut && _currentLoop.clipOut)
			{
				if(oneShot)
				{
					PlayOneShot(_currentLoop.clipOut);
				}

				else
				{
					Play(_currentLoop.clipOut);
				}

				return;
			}

			Debug.LogWarning($"Unable to play the loop-out of the current element, maybe you forgot to provide an out clip or the element isn't in loop mode", this);
		}

        private void ChangeAudio(AudioLoopable element)
        {
			_currentLoop = element;
            if(element.inLoopOut && element.clipIn)
			{
				PlayInLoop(element);
			}

			else if(element.inLoopOut)
			{
				PlayLoop(element.clip);
			}

			else
			{
				Play(element.clip);
			}
        }

        private void PlayInLoop(AudioLoopable element)
        {
            Play(element.clipIn);
			StartCoroutine(TrackEndOfAudio(element.clipIn));
        }

		private void PlayLoop(AudioClip clip)
		{
			_source.loop = true;
			_source.clip = clip;
			_source.Play();
			StopAllCoroutines();
		}

		private void Play(AudioClip clip)
		{
			_source.loop = false;
			_source.clip = clip;
			_source.Play();
			StopAllCoroutines();
		}
		
		private void PlayOneShot(AudioClip clip)
		{
			StopAllCoroutines();
			_source.PlayOneShot(clip);
		}

        public override void Pause()
        {
            _source.Pause();
        }

        public override void Unpause()
        {
            _source.UnPause();
        }

		public override void Stop()
		{
			StopAllCoroutines();
			_source.Stop();
		}

		#endregion


		#region Coroutines

		private IEnumerator TrackEndOfAudio(AudioClip inLoopClip)
        {
			yield return new WaitForSeconds(inLoopClip.length);

			PlayLoop(_currentLoop.clip);
        }
		
		#endregion


		#region Utils

		private AudioLoopable GetElement(string elementName)
		{
			foreach (var element in _audioLoops)
			{
				if(element.name.Equals(elementName))
				{
					return element;
				}
			}

			Debug.LogWarning($"'{elementName}' not found in the 'EntityAudioModule' module", this);
			return null;
		}
			 
		#endregion
		

        private AudioLoopable _currentLoop;
	}
}