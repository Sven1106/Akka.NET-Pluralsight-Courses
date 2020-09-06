using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
public static class ColorConsole
{
    private static object _MessageLock = new object();
    public static void WriteLine(string message, ConsoleColor color)
    {
        lock (_MessageLock)
        {
            ConsoleColor previousColor = Console.ForegroundColor;
            Console.ForegroundColor = color;
            Console.WriteLine(message);
            Console.ForegroundColor = previousColor;
        }
    }
}