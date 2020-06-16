namespace DebugConsole.Implementations.Console
{
    using System;

    /// <summary>
    ///   A <see cref="DebugConsole"/> handler for writing and reading from and to the console.
    /// </summary>
    public class ConsoleDebugConsoleHandler : DebugConsoleHandler
    {
        public override void Write(string value) => Console.Write(value);

        public override void WriteError(string value) => Console.Error.WriteLine(value);

        public override void WriteLine(string value) => Console.WriteLine(value);

        protected override void ReadLoop() => OnInput(Console.ReadLine());
    }
}
