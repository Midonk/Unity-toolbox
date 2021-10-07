using System.Collections.Generic;

public class Observable<T>
{
    public T Value
    {
        get => _value;
        set
        {
            _value = value;
            NotifyObservers();
        }
    }


    #region Main

    public void NotifyObservers()
    {
        foreach (var observer in _observers)
        {
            observer.Notify();
        }
    }

    public void Register(IObserver observer)
    {
        if(_observers.Contains(observer)) return;

        _observers.Add(observer);
    }
    
    public void Unregister(IObserver observer)
    {
        if(!_observers.Contains(observer)) return;

        _observers.Remove(observer);
    }
         
    #endregion

    
    #region Private Fields

    private List<IObserver> _observers;
    private T _value;
         
    #endregion
}