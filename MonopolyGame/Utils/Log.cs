using System.Diagnostics;

namespace MonopolyGame.Utils;


public static class Log
{
    [Conditional("DEBUG")]
    public static void WriteLine(string message)
    {
        Console.WriteLine("[ DEBUG ]: " + message);
    }

    [Conditional("DEBUG")]
    public static void WriteLine(object value)
    {
        Console.WriteLine(value);
    }
}
