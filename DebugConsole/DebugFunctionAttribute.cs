using System;
using System.Runtime.CompilerServices;

namespace DebugConsole
{
    [AttributeUsage(AttributeTargets.Method)]
    public class DebugFunctionAttribute : Attribute, IComparable
    {
        public string FunctionExecutionName { get; set; }
        public DebugFunctionAttribute([CallerMemberName]string functionExecutionName = "")
        {
            this.FunctionExecutionName = functionExecutionName;
        }

        public override bool Match(object obj) => Equals(obj);
        public override int GetHashCode() => FunctionExecutionName.GetHashCode();

        public override bool Equals(object obj)
        {
            if(!(obj is DebugFunctionAttribute dfa)) return false;
            return string.Equals(FunctionExecutionName, dfa.FunctionExecutionName);
        }

        public int CompareTo(object obj)
        {
            if (!(obj is DebugFunctionAttribute dfa)) throw new ArgumentException();
            return string.Compare(FunctionExecutionName, dfa.FunctionExecutionName);
        }
    }
}
