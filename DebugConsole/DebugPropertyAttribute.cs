namespace DebugConsole
{
    using System;
    using System.Runtime.CompilerServices;

    /// <summary>
    ///   An attribute to declare a public static property as read and writeable from the DebugConsole.
    /// </summary>
    [AttributeUsage(AttributeTargets.Property, AllowMultiple = true)]
    public class DebugPropertyAttribute : Attribute
    {
        /// <summary>
        ///   Initializes a new instance of the <see cref="DebugPropertyAttribute"/> class.
        /// </summary>
        /// <param name="propertyExecutionName">
        ///   The name of the property. Used by the DebugConsole.
        ///   Per default it is its implementation name.
        /// </param>
        /// <param name="mayWrite">
        ///   If the Set-command should be allowed for this property.
        ///   Ignored if the property is readonly.
        /// </param>
        public DebugPropertyAttribute([CallerMemberName] string propertyExecutionName = "", bool mayWrite = true)
        {
            this.MayWrite = mayWrite;
            this.PropertyExecutionName = propertyExecutionName;
        }

        /// <summary>
        ///   Gets the name of the property. Per default it is its implementation name.
        /// </summary>
        public string PropertyExecutionName { get; private set; }

        /// <summary>
        ///  Gets a value indicating whether the Set-command should be allowed for this property.
        ///  Ignored if the property is readonly.
        /// </summary>
        public bool MayWrite { get; private set; }
    }
}
