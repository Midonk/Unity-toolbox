using UnityEngine;
using UnityEngine.Events;

namespace AnimationUtils
{
	public class AnimationEventRaiser : MonoBehaviour
	{
		#region Exposed

		[SerializeField] private AnimationEventHandler[] _animationEventHandlers;
		[Range(0, 0.999f), Tooltip("If used with an Animator, minimal weight the animation clip should have to be allowed to raise events")]
		[SerializeField] private float _weightTreshold = 0.5f;
			 
		#endregion


		#region Main

		//Called by animation events
		/// <summary>
		/// 	Used to raise an event linked on an animation
		/// </summary>
		/// <param name="evt">Received event from the animation</param>
		public void RaiseEvent(AnimationEvent evt)
		{
			if(evt.isFiredByLegacy || evt.animatorClipInfo.weight <= _weightTreshold) return;
			
			var eventName = evt.stringParameter;
			foreach (var handler in _animationEventHandlers)
			{
				if(!handler.m_eventName.Equals(eventName)) continue;

				handler.m_onEventRaised?.Invoke();
				return;
			}
		}
			 
		#endregion
	

		[System.Serializable]
		private struct AnimationEventHandler
		{
			public string m_eventName;
			public UnityEvent m_onEventRaised;
		} 
	}
}