using UnityEngine;

public class SampleFixedUpdatable : ManagedBehaviour, IFixedUpdatable
{
    [SerializeField][Min(0)]
    private float _refreshTime;

    private void OnGUI() 
    {
        GUI.Button(
            new Rect(0, 0, 150, 25), 
            $"Fixed delta time: {_fixedDelta : #0.00}"
            )    ;
    }

    public void FixedTick()
    {
        var currentTime = Time.time;
        var refreshDelta = currentTime - _previousRefreshTime;
        if(refreshDelta >= _refreshTime)
        {
            _previousRefreshTime = currentTime;
            _fixedDelta = currentTime - _previousTime;
        }
        _previousTime = currentTime;
    }

    private float _previousRefreshTime;
    private float _previousTime;
    private float _fixedDelta;
}