using System;
using UnityEngine;

namespace StateMachine
{
    [Serializable]
    public class FiniteStateMachine : StateMachine
    {
        public FiniteStateMachine(State[] states) : base()
        {
            foreach (var state in states)
            {
                _availableStates.Add(state.GetType(), state);
            }
        }

        public override void ChangeState(Type requestedState)
        {
            if(!_availableStates.ContainsKey(requestedState))
			{
				Debug.LogError($"Missing state '{requestedState.ToString()}', a state tried to change to this state but it isn't available into the map.");
			}

            base.ChangeState(requestedState);
        }
    }
}