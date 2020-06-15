namespace DebugConsole.Implementations.Misc
{
    using System.Diagnostics;

    public class DebugDebugConsoleHandler : DebugConsoleHandler
    {
        public override void ReadLoop() { }
        public override void Write(string value) => Debug.Write(value);
        public override void WriteLine(string value) => Debug.WriteLine(value);
    }
}
