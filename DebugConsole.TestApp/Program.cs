using System.Threading;
using DebugConsole.ConsoleHandlers;

namespace DebugConsole.TestApp
{
    class Program
    {
        static void Main(string[] args)
        {
            DebugConsole.Handler = new ConsoleDebugConsoleHandler();



            Thread.Sleep(Timeout.Infinite);
        }
    }
}
