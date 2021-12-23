using System;

public static class SO_Utility
{
    /// <summary>
    /// convert first character to capitalize of given string
    /// </summary>
    /// <param name="input">Input string</param>
    /// <returns>First Character Capital string</returns>
    /// <exception cref="ArgumentNullException">Input is null</exception>
    /// <exception cref="ArgumentException">if input is empty</exception>
    public static string FirstCharToUpper(string input)
    {
        switch (input)
        {
            case null: throw new ArgumentNullException(nameof(input));
            case "": throw new ArgumentException($"{nameof(input)} cannot be empty", nameof(input));
            default: return input[0].ToString().ToUpper() + input.Substring(1);
        }
    }
}
