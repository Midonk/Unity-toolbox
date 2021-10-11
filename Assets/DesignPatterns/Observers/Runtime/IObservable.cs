using System.Collections.Generic;

namespace Patterns.Observer
{
    public class Observable<T>
    {
        #region Properties

        public T Value
        {
            get => _value;
            set
            {
                _value = value;
                NotifyObservers(value);
            }
        }
             
        #endregion


        #region Main

        private void NotifyObservers(T value)
        {
            for (int i = 0; i < _observers.Count; i++)
            {
                _observers[i].Notify(value);
            }
        }

        public void Register(IObserver<T> observer)
        {
            if(_observers.Contains(observer)) return;

            _observers.Add(observer);
        }
        
        public void Unregister(IObserver<T> observer)
        {
            if(!_observers.Contains(observer)) return;

            _observers.Remove(observer);
        }
            
        #endregion

        
        #region Private Fields

        private List<IObserver<T>> _observers;
        private T _value;
            
        #endregion
    }
}