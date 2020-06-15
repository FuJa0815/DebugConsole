namespace DebugConsole
{
    /// <summary>
    ///   Handles IO for th debug console.
    /// </summary>
    public abstract class DebugConsoleHandler
    {
        protected void OnInput(string line)
        {
            // TODO: Handle console input
        }
        public abstract void WriteLine(string value);
        public abstract void Write(string value);
        public abstract void ReadLoop();
    }
}