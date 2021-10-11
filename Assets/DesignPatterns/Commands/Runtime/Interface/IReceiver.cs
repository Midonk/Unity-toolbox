namespace Patterns.Command
{
    public interface IReceiver<T> where T : IReceiver<T>
    {
        public abstract void ExecuteCommand(ICommand<T> command);
    }
}