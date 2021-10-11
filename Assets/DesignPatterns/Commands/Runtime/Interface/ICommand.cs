namespace Patterns.Command
{
    public interface ICommand<T> where T : IReceiver<T>
    {
        public void Execute(T receiver);
    }
}