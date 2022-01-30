using UnityEngine;
using System.Collections;

public class InLoopOutAudioPlayer : AudioPlayer<LoopableAudioElement>, IAudioPlayer
{
	#region Main

	public override void Play()
	{
		var element = (ILoopableAudioElement)_currentElement;
		element.SetupSource(_source);
		
		switch (element.Phase)
		{
			case LoopPhase.In:
				PlayInLoop();
				break;
			
			case LoopPhase.Loop:
				PlayLoop();
				break;
			
			case LoopPhase.Out:
				PlayLoopOut();
				break;
		}
	}

	public void PlayLoopOut(bool oneShot = false)
	{
		if(oneShot)
		{
			PlayOneShot(_currentElement);
		}

		else
		{
        	StopAllCoroutines();
			_source.Play();
		}
	}

	private void PlayInLoop()
	{
		StartCoroutine(TrackEndOfAudio(_source.clip));
		_source.Play();
	}

	private void PlayLoop()
	{
		_source.Play();
	}
	
	public override void PlayOneShot(IAudioElement element)
    {
        StopAllCoroutines();
		_source.Stop();
		base.PlayOneShot(element);
    }

	public override void Stop()
	{
		StopAllCoroutines();
		base.Stop();
	}

	#endregion


	#region Coroutines

	private IEnumerator TrackEndOfAudio(AudioClip trackedClip)
	{
		yield return new WaitForSeconds(trackedClip.length);
	}

    #endregion
}