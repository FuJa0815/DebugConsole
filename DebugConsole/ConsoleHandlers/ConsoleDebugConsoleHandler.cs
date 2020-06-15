namespace DebugConsole.ConsoleHandlers
{
    using System;
    using System.Collections.Generic;
    using System.IO;
    using System.Linq;
    using System.Text;

    public class ConsoleDebugConsoleHandler : DebugConsoleHandler
    {
        public override void ReadLoop() => OnInputEvent(Console.ReadLine());
        public override void Write(string value) => Console.Write(value);
        public override void WriteLine(string value) => Console.WriteLine(value);
    }
}
