namespace DebugConsole.ConsoleHandlers
{
    using System;
    using System.Collections.Generic;
    using System.Diagnostics;
    using System.IO;
    using System.Linq;
    using System.Text;

    public class DebugDebugConsoleHandler : DebugConsoleHandler
    {
        public override void ReadLoop() { }
        public override void Write(string value) => Debug.Write(value);
        public override void WriteLine(string value) => Debug.WriteLine(value);
    }
}
