namespace DebugConsole
{
    using System;
    using System.Diagnostics.Tracing;
    using System.IO;
    using System.Threading.Tasks;

    /// <summary>
    ///   Handles IO for th debug console.
    /// </summary>
    public abstract class DebugConsoleHandler
    {
        protected void OnInputEvent(string line)
        {
            // TODO: Handle console input
        }
        public abstract void WriteLine(string value);
        public abstract void Write(string value);
        public abstract void ReadLoop();
    }
}