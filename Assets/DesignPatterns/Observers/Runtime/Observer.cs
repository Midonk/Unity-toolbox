using System;

namespace Patterns.Observer
{
    public class Observer<T>
    {
        public Observer(Action<T> notification)
        {
            _notification = notification;
        }

        /// <summary>
        ///     Notify the observer that the value he watches just changed
        /// </summary>
        /// <param name="value">New observed value</param>
        public void Notify(T value)
        {
            _notification.Invoke(value);
        }

        private Action<T> _notification;
    }
}