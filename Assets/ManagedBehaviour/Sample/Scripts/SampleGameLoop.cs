using UnityEngine;

public class SampleGameLoop : MonoBehaviour
{
    private void Awake() 
    {
        _updatManager = UpdateManager.Instance;
    }

    private void Update() 
    {
        _updatManager.Tick();
    }

    private void FixedUpdate() 
    {
        _updatManager.FixedTick();
    }

    private UpdateManager _updatManager;
}