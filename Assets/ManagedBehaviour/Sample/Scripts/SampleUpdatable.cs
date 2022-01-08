using UnityEngine;

public class SampleUpdatable : ManagedBehaviour, IUpdatable, IScheduledUpdate
{
    [SerializeField][Min(1)]
    private int _frameInterval;

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
        _delta = currentTime - _previousTime;
        _previousTime = currentTime;
    }

    private float _previousTime;
    private float _delta;

    public int FrameInterval => _frameInterval;
}