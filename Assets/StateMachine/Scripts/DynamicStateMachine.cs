
namespace StateMachine
{
    public class DynamicStateMachine : StateMachine
    {
        public void AddState(State newState)
        {
            if (_availableStates.ContainsValue(newState)) return;
            
            _availableStates.Add(newState.GetType(), newState);
        }
    }
}