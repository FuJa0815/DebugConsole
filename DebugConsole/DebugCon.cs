namespace DebugConsole
{
    using System.Threading;

    /// <summary>
    ///   The main class of the libary.
    /// </summary>
    public static class DebugCon
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

        /// <inheritdoc cref="DebugCon.WriteLine(string)"/>
        public static void WriteLine() => DebugCon.WriteLine(string.Empty);

        /// <inheritdoc cref="DebugCon.WriteLine(object)"/>
        public static void WriteLine(object value) => DebugCon.WriteLine(value);

        /// <inheritdoc cref="DebugCon.WriteLine(string)"/>
        public static void WriteLine(string value) => Handler.WriteLine(value);

        /// <inheritdoc cref="DebugCon.WriteError(string)"/>
        public static void WriteError(string value) => Handler.WriteError(value);

        /// <inheritdoc cref="DebugCon.Write(string)"/>
        public static void Write(string value) => Handler.Write(value);

        /// <inheritdoc cref="DebugCon.Write(object)"/>
        public static void Write(object value) => DebugCon.Write(value);
    }
}
