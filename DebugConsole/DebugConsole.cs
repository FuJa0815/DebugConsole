using System.Threading;
using System.Threading.Tasks;

namespace DebugConsole
{
    /// <summary>
    ///   The main class of the libary.
    /// </summary>
    public static class DebugConsole
    {
        private static DebugConsoleHandler handler;
        private static Thread readThread;

        /// <summary>
        ///   Gets or sets the handler for all inputs and outputs of the console.
        /// </summary>
        public static DebugConsoleHandler Handler
        {
            get => handler;
            set
            {
                readThread?.Abort();
                handler = value;
                readThread =
                    new Thread(
                        new ThreadStart(() =>
                        {
                            while (Thread.CurrentThread.IsAlive)
                                handler.ReadLoop();
                        }));
                readThread.Start();
            }
        }

        #region WriteLine
        public static void WriteLine() => DebugConsole.WriteLine(string.Empty);
        public static void WriteLine(object value) => DebugConsole.WriteLine(value.ToString());
        public static void WriteLine(string value) => Handler.WriteLine(value);
        #endregion WriteLine
        #region Write
        public static void Write(string value) => Handler.Write(value);
        public static void Write(object value) => DebugConsole.Write(value.ToString());
        #endregion Write
    }
}
