using System.Threading;

namespace DebugConsole
{
    /// <summary>
    ///   Handles IO for th debug console.
    /// </summary>
    public abstract class DebugConsoleHandler
    {
        /// <inheritdoc cref="LineParser.OnInput(string)"/>
        protected void OnInput(string line) => LineParser.OnInput(line);
        /// <summary>
        ///   Write a line to the DebugMenu
        /// </summary>
        /// <param name="value">
        ///   The line to be written
        /// </param>
        public abstract void WriteLine(string value);
        /// <summary>
        ///   Write chars to the DebugMenu
        /// </summary>
        /// <param name="value">
        ///   The chars to be written
        /// </param>
        public abstract void Write(string value);
        /// <summary>
        ///   Write an error to the DebugMenu
        /// </summary>
        /// <param name="value">
        ///   The error to be written
        /// </param>
        public abstract void WriteError(string value);
        /// <summary>
        ///   This function gets executed in a while loop in a seperate thread.
        ///   Execute OnInput if you got input from your DebugConsole.
        ///   Per default it halts the seperate thread.
        /// </summary>
        internal protected virtual void ReadLoop() => Thread.Sleep(Timeout.Infinite);
    }
}