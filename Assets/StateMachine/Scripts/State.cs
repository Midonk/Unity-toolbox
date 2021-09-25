namespace StateMachine
{
	public abstract class State
	{
		public State() {}

		public virtual void Enter(){}
		public virtual void Update(){}
		public virtual void FixedUpdate(){}
		public virtual void Exit(){}

	}
}