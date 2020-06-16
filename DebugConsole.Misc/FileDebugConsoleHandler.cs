using System.IO;

namespace DebugConsole.Implementations.Misc
{
    /// <summary>
    ///   A <see cref="DebugConsole"/> handler for writing to a file.
    /// </summary>
    public class FileDebugConsoleHandler : DebugConsoleHandler
    {
        /// <param name="filename">
        ///   The file to be written to
        /// </param>
        public FileDebugConsoleHandler(string filename)
        {
            sw = new StreamWriter(File.OpenRead(filename));
        }
        private StreamWriter sw;

        public override void Write(string value) => sw.Write(value);
        public override void WriteLine(string value) => sw.WriteLine(value);
        public override void WriteError(string value) => sw.WriteLine(value);
    }
}
