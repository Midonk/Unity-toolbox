using UnityEngine;
using Patterns.Observer;

namespace ScriptableVariables
{
    public abstract class ObservableVariable<T> : ScriptableVariable<T>
    {
        #region Exposed

		[SerializeField]
		private Observable<T> _observable;
			 
		#endregion

		
		#region Properties

		public override T Value
		{
			get => _observable.Value;
			set => _observable.Value = value;
		}
		
		#endregion


        //_value from parent not used anywhere
    }
}