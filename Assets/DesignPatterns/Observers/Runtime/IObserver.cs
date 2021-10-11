
namespace Patterns.Observer
{
    public interface IObserver<T>
    {
        /// <summary>
        ///     Notify the observer that the value he watches just changed
        /// </summary>
        /// <param name="value">New observed observable value</param>
        public void Notify(T value);
    }
}