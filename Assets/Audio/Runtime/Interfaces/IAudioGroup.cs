using Utils;

public interface IAudioGroup<T> where T : IAudioGroupItem
{
    SmartList<T> Players { get; }

    void Add(T item);
    void Remove(T item);
}