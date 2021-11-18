using UnityEngine;

public class SampleUpdatable : ManagedBehaviour, IUpdatable, IScheduledUpdate
{
    [SerializeField][Min(0)]
    private float _refreshTime;

    private void OnGUI() 
    {
        GUI.Button(
            new Rect(0, 28, 150, 25), 
            $"Delta time: {_delta : #0.0000}"
            );
    }

    public void Tick()
    {
        var currentTime = Time.time;
        var refreshDelta = currentTime - _previousRefreshTime;
        if(refreshDelta >= _refreshTime)
        {
            _previousRefreshTime = currentTime;
            _delta = currentTime - _previousTime;
        }
        _previousTime = currentTime;
    }

    private float _previousTime;
    private float _previousRefreshTime;
    private float _delta;

    public int FrameInterval => 100;
}