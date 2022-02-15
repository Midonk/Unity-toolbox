
//Interface to allow any IUpdatableBehaviour to be updated on a scheduled frame interval by the UpdateManager

public interface IScheduledUpdate
{
    int FrameInterval { get; }
}