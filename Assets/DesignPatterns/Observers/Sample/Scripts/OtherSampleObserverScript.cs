using Patterns.Observer;
using UnityEngine;

public class OtherSampleObserverScript : MonoBehaviour
{
    public Observable<float> Observable => _observable;

    private void OnGUI() 
    {
        if(GUILayout.Button("Increment"))
        {
            _observable.Value++;
        }    
    }
    
    private Observable<float> _observable = new Observable<float>();
}