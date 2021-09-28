using UnityEngine;

namespace ScriptableVaribles
{
	public abstract class ScriptableVariable<T> : ScriptableObject
	{
		#region Exposed

		[SerializeField]
		private T _defaultValue;

		[SerializeField]
		private T _value;
		
		[SerializeField]
		private bool _resetValueOnExitPlayMode;
			 
		#endregion

		
		#region Properties

		public T Value
		{
			get => _value;
			set => _value = value;
		}
		
		#endregion

		
		#region Unity API

		private void OnEnable() 
		{
			if(!_resetValueOnExitPlayMode) return;

			Value = _defaultValue;		
		}

		private void OnDisable() 
		{
			if(!_resetValueOnExitPlayMode) return;

			Value = _defaultValue;	
		}

		#endregion
	}
}