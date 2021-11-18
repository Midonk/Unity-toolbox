
//Interface to allow any ManagedBehaviour to be fixed updated by the UpdateManager

public interface IFixedUpdatable : IUpdatableBehaviour
{
    public void FixedTick();
}