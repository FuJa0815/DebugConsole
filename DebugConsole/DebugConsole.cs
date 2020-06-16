namespace DebugConsole
{
    using System.Threading;

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
                            {
                                handler.ReadLoop();
                            }
                        }));
                readThread.Start();
            }
        }

        /// <inheritdoc cref="DebugConsole.WriteLine(string)"/>
        public static void WriteLine() => DebugConsole.WriteLine(string.Empty);

        /// <inheritdoc cref="DebugConsole.WriteLine(object)"/>
        public static void WriteLine(object value) => DebugConsole.WriteLine(value);

        /// <inheritdoc cref="DebugConsole.WriteLine(string)"/>
        public static void WriteLine(string value) => Handler.WriteLine(value);

        /// <inheritdoc cref="DebugConsole.WriteError(string)"/>
        public static void WriteError(string value) => Handler.WriteError(value);

        /// <inheritdoc cref="DebugConsole.Write(string)"/>
        public static void Write(string value) => Handler.Write(value);

        /// <inheritdoc cref="DebugConsole.Write(object)"/>
        public static void Write(object value) => DebugConsole.Write(value);
    }
}
