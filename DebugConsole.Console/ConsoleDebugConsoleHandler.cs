namespace DebugConsole.Implementations.ConsoleHandlers
{
    using System;

    public class ConsoleDebugConsoleHandler : DebugConsoleHandler
    {
        public override void ReadLoop() => OnInput(Console.ReadLine());
        public override void Write(string value) => Console.Write(value);
        public override void WriteLine(string value) => Console.WriteLine(value);
    }
}
