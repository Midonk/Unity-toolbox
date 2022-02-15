
//Interface to allow any ManagedBehaviour to be updated by the UpdateManager

public interface IUpdatable : IUpdatableBehaviour
{
    public void Tick();
}