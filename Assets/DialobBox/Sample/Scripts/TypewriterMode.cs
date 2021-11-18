using UnityEngine;

public class TypewriterMode : IDialogDisplayMode
{
    public string GetText(string textToDisplay, int lastIndex)
    {
        return textToDisplay.Substring(0, lastIndex);
    }

    bool IDialogDisplayMode.IsDisplaying(string textToDisplay, int lastIndex)
    {
        return lastIndex < textToDisplay.Length;
    }
}