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
        ///   Initializes a new instance of the <see cref="FileDebugHandler"/> class.
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
