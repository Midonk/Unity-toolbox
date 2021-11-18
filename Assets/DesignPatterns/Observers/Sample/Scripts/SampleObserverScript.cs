using UnityEngine;
using Patterns.Observer;

public class SampleObserverScript : MonoBehaviour
{
    [SerializeField]
    private OtherSampleObserverScript _observableScript;
    private Observer<float> _valueObserver;

    private void Awake() 
    {
        _valueObserver = new Observer<float>(YieldChange);
    }

    private void OnEnable() 
    {
        _observableScript.Observable.Register(_valueObserver);   
    }

    private void OnDisable() 
    {
        _observableScript.Observable.Unregister(_valueObserver);
    }

    private void YieldChange(float value)
    {
        Debug.Log($"The observed value is now {value}");
    }
}