namespace DebugConsole.Implementations.Console
{
    using System;

    /// <summary>
    ///   A <see cref="DebugCon"/> handler for writing and reading from and to the console.
    /// </summary>
    public class ConsoleDebugHandler : DebugConsoleHandler
    {
        /// <inheritdoc/>
        public override void Write(string value) => Console.Write(value);

        /// <inheritdoc/>
        public override void WriteError(string value) => Console.Error.WriteLine(value);

        /// <inheritdoc/>
        public override void WriteLine(string value) => Console.WriteLine(value);

        /// <inheritdoc/>
        protected override void ReadLoop() => this.OnInput(Console.ReadLine());
    }
}
