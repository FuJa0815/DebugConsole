using System;
using System.Diagnostics;
using DebugConsole.ConsoleHandlers;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace DebugConsole.Test
{
    [TestClass]
    public class ConsoleDebugConsoleHandlerTest
    {
        [TestMethod]
        public void Test()
        {
            DebugConsole.Handler = new ConsoleDebugConsoleHandler();
        }
    }
}
