using DebugConsole.Implementations.Console;
using System.Threading;

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
