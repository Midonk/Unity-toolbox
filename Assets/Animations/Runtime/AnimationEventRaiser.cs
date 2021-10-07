using UnityEngine;
using UnityEngine.Events;

namespace AnimationUtils
{
	public class AnimationEventRaiser : MonoBehaviour
	{
		#region Exposed

		[SerializeField]
		private AnimationEventHandler[] _animationEventHandlers;

		[SerializeField][Range(0, 0.999f)]
		private float _weightTreshold = 0.5f;
			 
		#endregion


		#region Main

		//Called by animation events
		/// <summary>
		/// 	Used to raise an event linked on an animation
		/// </summary>
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
		public struct AnimationEventHandler
		{
			public string m_eventName;
			public UnityEvent m_onEventRaised;
		} 
	}
}