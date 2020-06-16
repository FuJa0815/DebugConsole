using DebugConsole.Implementations.Console;
using System.Threading;

namespace DebugConsole.TestApp
{
    class Program
    {
        static void Main(string[] args)
        {
            DebugCon.Handler = new ConsoleDebugHandler();
            Thread.Sleep(Timeout.Infinite);
        }

        [DebugFunction]
        public static void MyFunction(string a1, int i1)
        {
            for(int i = 0; i < i1; i++)
                DebugCon.WriteLine("You executed me with " + a1 + ". The first property is " + Text + ", the second one is " + ReadOnlyProperty);
        }

        [DebugProperty]
        public static string Text { get; set; } = "foo";

        [DebugProperty("Text2", false)]
        public static string ReadOnlyProperty { get; set; } = "bar";
    }
}
