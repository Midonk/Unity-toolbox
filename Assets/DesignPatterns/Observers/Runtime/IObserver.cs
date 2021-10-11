
namespace Patterns.Observer
{
    public interface IObserver<T>
    {
        public void Notify(T value);
    }
}