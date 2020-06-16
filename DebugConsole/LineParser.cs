namespace DebugConsole
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Reflection;
    using System.Runtime.InteropServices;

    internal static class LineParser
    {
        [DllImport("shell32.dll", SetLastError = true)]
        static extern IntPtr CommandLineToArgvW(
            [MarshalAs(UnmanagedType.LPWStr)] string lpCmdLine, out int pNumArgs);

        public static object[] CommandLineToArgs(string commandLine)
        {
            int argc;
            var argv = CommandLineToArgvW(commandLine, out argc);
            if (argv == IntPtr.Zero)
                throw new System.ComponentModel.Win32Exception();
            try
            {
                var args = new string[argc];
                for (var i = 0; i < args.Length; i++)
                {
                    var p = Marshal.ReadIntPtr(argv, i * IntPtr.Size);
                    args[i] = Marshal.PtrToStringUni(p);
                }

                return args;
            }
            finally
            {
                Marshal.FreeHGlobal(argv);
            }
        }

        static LineParser()
        {
            methods = Assembly.GetEntryAssembly().GetTypes()
                      .AsParallel()
                      .SelectMany(t => t.GetMethods(BindingFlags.Static | BindingFlags.Public))
                      .ToDictionary(p => p.GetCustomAttributes(typeof(DebugFunctionAttribute), false), p => p)
                      .SelectMany(kv => kv.Key.Select(p => new KeyValuePair<DebugFunctionAttribute, MethodInfo>((DebugFunctionAttribute)p, kv.Value)))
                      .ToDictionary(p => p.Key, p => p.Value);
            properties = Assembly.GetEntryAssembly().GetTypes()
                      .AsParallel()
                      .SelectMany(t => t.GetProperties(BindingFlags.Static | BindingFlags.Public))
                      .ToDictionary(p => p.GetCustomAttributes(typeof(DebugPropertyAttribute), false), p => p)
                      .SelectMany(kv => kv.Key.Select(p => new KeyValuePair<DebugPropertyAttribute, PropertyInfo>((DebugPropertyAttribute)p, kv.Value)))
                      .ToDictionary(p => p.Key, p => p.Value);
        }

        private static readonly IDictionary<DebugFunctionAttribute, MethodInfo> methods;

        internal static readonly IDictionary<DebugPropertyAttribute, PropertyInfo> properties = Assembly.GetEntryAssembly().GetTypes()
                      .AsParallel()
                      .SelectMany(t => t.GetProperties(BindingFlags.Static | BindingFlags.Public))
                      .ToDictionary(p => p.GetCustomAttributes(typeof(DebugPropertyAttribute), false), p => p)
                      .SelectMany(kv => kv.Key.Select(p => new KeyValuePair<DebugPropertyAttribute, PropertyInfo>((DebugPropertyAttribute)p, kv.Value)))
                      .ToDictionary(p => p.Key, p => p.Value);

        public static void OnInput(string line)
        {
            var args = CommandLineToArgs(line);
            var meth = methods.FirstOrDefault(p => p.Key.FunctionExecutionName == args[0].ToString());
            var args2 = new object[args.Length - 1];
            Array.Copy(args, 1, args2, 0, args.Length - 1);
            if (meth.Equals(default(KeyValuePair<DebugFunctionAttribute, MethodInfo>)))
            {
                DebugConsole.WriteError($"Method {args[0]} not found");
                return;
            }
            var param = meth.Value.GetParameters();
            if (param.Length != args2.Length)
            {
                DrawHelp(meth.Value.Name, param);
                return;
            }
            for (var i = 0; i < param.Length; i++)
            {
                args2[i] = Convert.ChangeType(args2[i], param[i].ParameterType, System.Globalization.CultureInfo.InvariantCulture);
            }
            try
            {
                meth.Value.Invoke(null, args2);
            }
            catch (Exception ex)
            {
                DebugConsole.WriteError(ex.Message);
            }
        }
        private static void DrawHelp(string method, ParameterInfo[] pi)
        {
            DebugConsole.WriteLine("Usage for " + method);
            foreach (var p in pi)
            {
                DebugConsole.WriteLine(p.Name + " typeof " + p.ParameterType.ToString());
            }
        }
    }
}
