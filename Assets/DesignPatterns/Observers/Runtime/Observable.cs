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
            for (int i = _observers.Count - 1; i >= 0 ; i--)
            {
                _observers[i].Notify(value);
            }
        }

        /// <summary>
        ///     Register a new observer to this observable
        /// </summary>
        /// <param name="observer">Registering observer</param>
        public void Register(Observer<T> observer)
        {
            if(_observers.Contains(observer)) return;

            _observers.Add(observer);
        }
        
        /// <summary>
        ///     Unregister a new observer to this observable
        /// </summary>
        /// <param name="observer">Unregistering observer</param>
        public void Unregister(Observer<T> observer)
        {
            if(!_observers.Contains(observer)) return;

            _observers.Remove(observer);
        }
            
        #endregion

        
        #region Private Fields

        private List<Observer<T>> _observers = new List<Observer<T>>();
        private T _value;
            
        #endregion
    }
}