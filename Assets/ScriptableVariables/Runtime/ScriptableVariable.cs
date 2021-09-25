using UnityEngine;

namespace ScriptableVaribles
{
	public abstract class ScriptableVariable<T> : ScriptableObject
	{
		#region Exposed

		[SerializeField]
		private T _defaultValue;

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

		private void OnDisable() 
		{
			if(!_resetValueOnExitPlayMode) return;

			Value = _defaultValue;	
		}

		#endregion

		
		#region Private Fields
		
		private T _value;
			 
		#endregion
	}
}