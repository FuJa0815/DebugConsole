namespace DebugConsole
{
    /// <summary>
    ///   Handles IO for th debug console.
    /// </summary>
    public abstract class DebugConsoleHandler
    {
        protected void OnInput(string line) => LineParser.OnInput(line);
        public abstract void WriteLine(string value);
        public abstract void Write(string value);
        public abstract void WriteError(string value);
        public abstract void ReadLoop();
    }
}