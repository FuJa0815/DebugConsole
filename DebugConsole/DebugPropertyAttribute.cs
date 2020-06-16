using System;
using System.Runtime.CompilerServices;

namespace DebugConsole
{
    [AttributeUsage(AttributeTargets.Property)]
    public class DebugPropertyAttribute : Attribute
    {
        public string PropertyExecutionName { get; set; }
        public DebugPropertyAttribute([CallerMemberName] string propertyExecutionName = "")
        {
            this.PropertyExecutionName = propertyExecutionName;
        }

        public override bool Match(object obj) => Equals(obj);
        public override int GetHashCode() => PropertyExecutionName.GetHashCode();

        public override bool Equals(object obj)
        {
            if (!(obj is DebugFunctionAttribute dfa)) return false;
            return string.Equals(PropertyExecutionName, dfa.FunctionExecutionName);
        }

        public int CompareTo(object obj)
        {
            if (!(obj is DebugFunctionAttribute dfa)) throw new ArgumentException();
            return string.Compare(PropertyExecutionName, dfa.FunctionExecutionName);
        }
    }
}
