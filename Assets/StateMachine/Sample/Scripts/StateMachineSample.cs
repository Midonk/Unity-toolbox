using UnityEngine;

namespace StateMachine
{
    public class StateMachineSample : MonoBehaviour
    {
        [SerializeField]
        private FiniteStateMachine _stateMachine;

        private void Awake() 
        {
            _stateMachine = new FiniteStateMachine(new State[]{new SampleState(), new OtherSampleState()});
            _stateMachine.ChangeState(typeof(SampleState));
        }

        private void OnGUI() 
        {
            if(GUILayout.Button("Change"))
            {
                if(_stateMachine.CurrentState.GetType().Equals(typeof(OtherSampleState)))
                    _stateMachine.ChangeState(typeof(SampleState));

                else
                {
                    _stateMachine.ChangeState(typeof(OtherSampleState));
                }
            }
        }
    }
}