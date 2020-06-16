namespace DebugConsole.Implementations.Misc
{
    using System.Diagnostics;

    /// <summary>
    ///   A <see cref="DebugConsole"/> handler for writing to the IDEs Debug window.
    /// </summary>
    public class DebugDebugConsoleHandler : DebugConsoleHandler
    {
        public override void Write(string value) => Debug.Write(value);

        public override void WriteError(string value) => WriteLine(value);

        public override void WriteLine(string value) => Debug.WriteLine(value);
    }
}
