using System;

public class WordMode : IDialogDisplayMode
{
    public string GetText(string textToDisplay, int lastIndex)
    {
        var words = textToDisplay.Split(' ');
        Array.Resize(ref words, lastIndex);

        var output = "";
        foreach (var word in words)
        {
            output += $"{word} ";
        }

        return output;
    }

    public bool IsDisplaying(string textToDisplay, int lastIndex)
    {
        var words = textToDisplay.Split(' ');
        return lastIndex < words.Length;
    }
}