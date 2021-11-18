using System.Collections.Generic;

public interface IDialogDisplayMode
{
    float DisplayTextPortionPerSecond { get; }
    string GetNextTextToDisplay(Queue<char> globalText);
}