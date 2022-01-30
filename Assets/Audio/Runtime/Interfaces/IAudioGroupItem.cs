public interface IAudioGroupItem 
{
    IAudioGroup<IAudioGroupItem>[] Groups { get; }
    
    void RegisterToGroups();    
    void UnregisterToGroups();    
}