namespace DebugConsole
{
    using System;
    using System.Runtime.CompilerServices;

    /// <summary>
    ///   An attribute to declare a public static method as callable from the DebugConsole
    /// </summary>
    [AttributeUsage(AttributeTargets.Method, AllowMultiple = true)]
    public class DebugFunctionAttribute : Attribute
    {
        /// <summary>
        ///   The name of the method. Per default it is its implementation name.
        /// </summary>
        public string FunctionExecutionName { get; set; }

        /// <param name="functionExecutionName">
        ///   The name of the method. Used by the DebugConsole.
        ///   Per default it is its implementation name.
        /// </param>
        public DebugFunctionAttribute([CallerMemberName]string functionExecutionName = "")
        {
            this.FunctionExecutionName = functionExecutionName;
        }
    }
}
