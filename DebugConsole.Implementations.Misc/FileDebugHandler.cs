namespace DebugConsole.Implementations.Misc
{
    using System.IO;

    /// <summary>
    ///   A <see cref="DebugConsole"/> handler for writing to a file.
    /// </summary>
    public class FileDebugHandler : DebugConsoleHandler
    {
        private readonly StreamWriter sw;

        /// <summary>
        ///   Creates a new FileDebugHandler for writing Debug-Info into a file.
        /// </summary>
        /// <param name="filename">
        ///   The file to be written to.
        /// </param>
        public FileDebugHandler(string filename)
        {
            this.sw = new StreamWriter(File.OpenRead(filename));
        }

        /// <inheritdoc/>
        public override void Write(string value) => this.sw.Write(value);

        /// <inheritdoc/>
        public override void WriteLine(string value) => this.sw.WriteLine(value);

        /// <inheritdoc/>
        public override void WriteError(string value) => this.sw.WriteLine(value);
    }
}
