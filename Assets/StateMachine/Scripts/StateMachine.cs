using System.Collections.Generic;
using System;

namespace StateMachine
{
	public abstract class StateMachine
    {
        public StateMachine()
        {
			_availableStates = new Dictionary<Type, State>();
        }


		#region Properties

		public State CurrentState => _currentState;
		public State PreviousState => _previousState;
			 
		#endregion


        #region Main

		public virtual void ChangeState(Type requestedState)
        {
            _currentState?.Exit();
			_previousState = _currentState;
			_currentState = _availableStates[requestedState];
			_currentState.Enter();
        }

		public void UpdateState()
		{
			_currentState?.Update();
		}

		public void FixedUpdateState()
		{
			_currentState?.FixedUpdate();
		}
			 
		#endregion
        

		#region Private Fields

		private State _currentState;
		private State _previousState;
		protected Dictionary<Type, State> _availableStates;
			 
		#endregion
    }
}