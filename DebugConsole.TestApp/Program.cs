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

        [DebugFunction]
        public static void MyFunction(string a1, int i1)
        {
            for(int i = 0; i < i1; i++)
                DebugConsole.WriteLine("you executed me with " + a1);
        }
    }
}
