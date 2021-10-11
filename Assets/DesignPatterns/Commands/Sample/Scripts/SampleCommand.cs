using Patterns.Command;

public class SampleCommand : ILoggableCommand<SampleReceiver>
{
    public string Info => $"This sample has been activated {_receiver.ActivationCount} times";

    public void Execute(SampleReceiver receiver)
    {
        _receiver = receiver;
        _receiver.DoSomething();
    }

    private SampleReceiver _receiver;
}