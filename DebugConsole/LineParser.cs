﻿namespace DebugConsole
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Reflection;
    using System.Runtime.InteropServices;

    /// <summary>
    ///   Helper for parsing and executing a line from a DebugConsole
    /// </summary>
    internal static class LineParser
    {
        [DllImport("shell32.dll", SetLastError = true)]
        static extern IntPtr CommandLineToArgvW(
            [MarshalAs(UnmanagedType.LPWStr)] string lpCmdLine, out int pNumArgs);

        /// <summary>
        ///   Seperates a line into attributes
        /// </summary>
        /// <param name="commandLine">
        ///   The line to be parsed
        /// </param>
        /// <returns>
        ///   An array of attributes
        /// </returns>
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
            var types = Assembly.GetEntryAssembly().GetTypes();

            methods = types.AsParallel()
                      // Get all public static methods
                      .SelectMany(t => t.GetMethods(BindingFlags.Static | BindingFlags.Public))
                      // Get all methods with the DebugFunction attribute
                      // Dictionary<DebugFunctiononAttribute[], MethodInfo>
                      .ToDictionary(p => p.GetCustomAttributes(typeof(DebugFunctionAttribute), false) as DebugFunctionAttribute[], p => p)
                      // Flatten the dictionary
                      .SelectMany(kv => kv.Key.Select(p => new KeyValuePair<DebugFunctionAttribute, MethodInfo>(p, kv.Value)))
                      .ToDictionary(p => p.Key, p => p.Value);

            properties = types.AsParallel()
                      // Get all public static properties
                      .SelectMany(t => t.GetProperties(BindingFlags.Static | BindingFlags.Public))
                      // Get all properties with the DebugProperty attribute
                      // Dictionary<DebugPropertyAttribute[], PropertyInfo>
                      .ToDictionary(p => p.GetCustomAttributes(typeof(DebugPropertyAttribute), false) as DebugPropertyAttribute[], p => p)
                      // Flatten the dictionary
                      .SelectMany(kv => kv.Key.Select(p => new KeyValuePair<DebugPropertyAttribute, PropertyInfo>(p, kv.Value)))
                      .ToDictionary(p => p.Key, p => p.Value);

            // Add the predefined functions
            foreach (var m in (typeof(PredefinedFunctions)).GetMethods(BindingFlags.Static | BindingFlags.Public))
                methods.Add(new KeyValuePair<DebugFunctionAttribute, MethodInfo>(new DebugFunctionAttribute(m.Name), m));
        }
        /// <summary>
        ///   Gets the PropertyInfo of a static property.
        /// </summary>
        /// <param name="name">
        ///   The name to be found.
        ///   Possible values:
        ///   <list type="bullet">
        ///     <item>property</item
        ///     <item>class.property</item>
        ///     <item>namespace.class.property</item>
        ///   </list>
        /// </param>
        /// <returns>
        ///   The info about the found property and true if its writeable
        /// </returns>
        /// <exception cref="FieldAccessException">
        ///   If the property could not be found.
        /// </exception>
        internal static (PropertyInfo, bool) GetProperty(string name)
        {
            var prop = LineParser.properties.FirstOrDefault(
                p => p.Key.PropertyExecutionName == name
                || p.Value.DeclaringType.Name + "." + p.Key.PropertyExecutionName == name
                || p.Value.DeclaringType.FullName + "." + p.Key.PropertyExecutionName == name);

            // If the property is not found
            if (prop.Equals(default(KeyValuePair<DebugPropertyAttribute, PropertyInfo>)))
                throw new FieldAccessException("Property " + name + " not found");

            return (prop.Value, prop.Key.MayWrite && prop.Value.CanWrite);
        }

        /// <summary>
        ///   Gets the MethodInfo of a static method.
        /// </summary>
        /// <param name="name">
        ///   The name to be found.
        ///   Possible values:
        ///   <list type="bullet">
        ///     <item>method</item
        ///     <item>class.method</item>
        ///     <item>namespace.class.method</item>
        ///   </list>
        /// </param>
        /// <returns>
        ///   The info about the found method.
        /// </returns>
        /// <exception cref="FieldAccessException">
        ///   If the method could not be found.
        /// </exception>
        internal static MethodInfo GetMethod(string name)
        {
            var meth = LineParser.methods.FirstOrDefault(
                p => p.Key.FunctionExecutionName == name
                || p.Value.DeclaringType.Name + "." + p.Key.FunctionExecutionName == name
                || p.Value.DeclaringType.FullName + "." + p.Key.FunctionExecutionName == name);

            // If the method is not found
            if (meth.Equals(default(KeyValuePair<DebugPropertyAttribute, PropertyInfo>)))
                throw new FieldAccessException("Method " + name + " not found");

            return meth.Value;
        }

        /// <summary>
        ///   The found DebugFunctions
        /// </summary>
        private static readonly IDictionary<DebugFunctionAttribute, MethodInfo> methods;


        /// <summary>
        ///   The found DebugProperties
        /// </summary>
        private static readonly IDictionary<DebugPropertyAttribute, PropertyInfo> properties;

        /// <summary>
        ///   Handle the line from the console.
        ///   Executes the method or prints an error to the DebugConsole.
        /// </summary>
        /// <param name="line">
        ///   The line to be handled.
        /// </param>
        public static void OnInput(string line)
        {
            if (string.IsNullOrWhiteSpace(line))
                return;

            // Get the args from the line
            // Example: Set property value
            var args = CommandLineToArgs(line);
            MethodInfo meth;
            try
            {
                meth = GetMethod(args[0].ToString());
            } catch(FieldAccessException ex)
            {
                // Method not found
                DebugConsole.WriteError(ex.Message);
                return;
            }
            // Init a new array with the method arguments.
            // Example: property value
            var args2 = new object[args.Length - 1];
            Array.Copy(args, 1, args2, 0, args.Length - 1);

            // Get the parameters of the method
            var param = meth.GetParameters();

            // Error if parameter length does not match
            if (param.Length != args2.Length)
            {
                DrawHelp(meth.Name, param);
                return;
            }

            // Convert every parameter into the correct type
            for (var i = 0; i < param.Length; i++)
            {
                args2[i] = Convert.ChangeType(args2[i], param[i].ParameterType, System.Globalization.CultureInfo.InvariantCulture);
            }

            try
            {
                // Try to invoke the method and print the return value
                var val = meth.Invoke(null, args2);
                if (val == null) return;
                DebugConsole.WriteLine(args[0] + " returned " + val.ToString());
            }
            catch (Exception ex)
            {
                // Something went wrong
                DebugConsole.WriteError((ex.InnerException ?? ex).Message);
            }
        }

        /// <summary>
        ///   Draws a method summary to the DebugConsole
        /// </summary>
        /// <param name="method">
        ///   The method to be explained
        /// </param>
        /// <param name="params">
        ///   The parameters of the method
        /// </param>
        private static void DrawHelp(string method, ParameterInfo[] @params)
        {
            DebugConsole.WriteLine("Usage for " + method);
            foreach (var p in @params)
            {
                DebugConsole.WriteLine(p.Name + " typeof " + p.ParameterType.ToString());
            }
        }
    }
}
