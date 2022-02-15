using UnityEngine;
using System.Collections;
using System;

public class InLoopOutAudioPlayer : AudioPlayer<LoopableAudioElement>, IAudioPlayer
{
    #region Main

    public override void Play()
	{
		var element = (ILoopableAudioElement)_currentElement;
		
		switch (element.Phase)
		{
			case LoopPhase.In:
				PlayLoop();
				break;
			
			case LoopPhase.Loop:
				PlayLoopOut();
				break;
			
			case LoopPhase.Out:
				PlayInLoop();
				break;
		}
	}

	public void PlayLoopOut(bool oneShot = false)
	{
		_currentElement.SetupSource(_source);

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
		_currentElement.SetupSource(_source);
		_onTrackEnded += Play;
		StartCoroutine(TrackEndOfAudio(_source.clip));
		_source.Play();
	}

	private void PlayLoop()
	{
		_currentElement.SetupSource(_source);
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
		_onTrackEnded?.Invoke();
		_onTrackEnded = null;
	}

    #endregion

    private Action _onTrackEnded;
}