namespace Patterns.Command
{
    public interface ILoggableCommand<T> : ICommand<T> where T : IReceiver<T>
    {
        string Info { get; }
    }
}