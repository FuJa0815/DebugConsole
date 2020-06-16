namespace DebugConsole.Implementations.Misc
{
    using System.Diagnostics;

    /// <summary>
    ///   A <see cref="DebugConsole"/> handler for writing to the IDEs Debug window.
    /// </summary>
    public class IdeDebugHandler : DebugConsoleHandler
    {
        /// <inheritdoc/>
        public override void Write(string value) => Debug.Write(value);

        /// <inheritdoc/>
        public override void WriteError(string value) => this.WriteLine(value);

        /// <inheritdoc/>
        public override void WriteLine(string value) => Debug.WriteLine(value);
    }
}
