using UnityEngine;

public class StringUtils
{
    public static string Colorize(Object str, string color)
    {
        return $"<color={color}>{str}</color>";
    }
}
