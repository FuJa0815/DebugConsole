using System;
using System.Collections.Generic;
using System.IO;
using System.Text;

namespace DebugConsole.ConsoleHandlers
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
