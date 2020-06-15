using System.IO;

namespace DebugConsole.Implementations.Misc
{
    public class FileDebugConsoleHandler : DebugConsoleHandler
    {
        public FileDebugConsoleHandler(string filename)
        {
            sw = new StreamWriter(File.OpenRead(filename));
        }
        private StreamWriter sw;

        public override void ReadLoop() { }
        public override void Write(string value) => sw.Write(value);
        public override void WriteLine(string value) => sw.WriteLine(value);
    }
}
