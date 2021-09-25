using UnityEngine;
using UnityEngine.Events;

public class GameEventListener : MonoBehaviour
{
    public GameEvent m_event;
    public UnityEvent m_response;

    private void OnEnable() 
    {
        m_event.Register(this);    
    }

    private void OnDisable() 
    {
        m_event.Unregister(this);    
    }

    public void OnEventRaised()
    {
        m_response?.Invoke();
    }
}