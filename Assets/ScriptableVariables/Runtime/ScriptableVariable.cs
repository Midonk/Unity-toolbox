using UnityEngine;


/*
* Add lil button next to the field (propertyDrawer) to allow user to enter a constant value
*/

namespace ScriptableVariables
{
	public abstract class ScriptableVariable<T> : ScriptableObject
	{
		#region Exposed

		[SerializeField]
		protected T _defaultValue;

		[SerializeField]
		protected T _value;
		
		[SerializeField]
		protected bool _resetValueOnExitPlayMode;
			 
		#endregion

		
		#region Properties

		public virtual T Value
		{
			get => _value;
			set => _value = value;
		}
		
		#endregion

		
		#region Unity API

		//may cause problems later because it's on enable
		protected virtual void OnEnable() 
		{
			if(!_resetValueOnExitPlayMode) return;

			Value = _defaultValue;		
		}

		//basically we only need this in editor right ?
		protected virtual void OnDisable() 
		{
			if(!_resetValueOnExitPlayMode) return;

			Value = _defaultValue;	
		}

		#endregion
	}
}