using System.Collections.Generic;

public interface IDialogDisplayMode
{
    bool IsDisplaying(string textToDisplay, int lastIndex);
    string GetText(string textToDisplay, int lastIndex);
}