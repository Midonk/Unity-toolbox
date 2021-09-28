using System;

namespace StateMachine
{
    public class DynamicStateMachine : StateMachine
    {
        public void AddState(State newState)
        {
            if (_availableStates.ContainsValue(newState)) return;
            
            _availableStates.Add(newState.GetType(), newState);
        }

        public override void ChangeState(Type requestedState)
        {
            if(!_availableStates.ContainsKey(requestedState))
			{
				AddState((State)Activator.CreateInstance(requestedState));
			}

            base.ChangeState(requestedState);
        }
    }
}