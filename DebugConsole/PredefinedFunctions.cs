namespace DebugConsole
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Reflection;

    public static class PredefinedFunctions
    {
        [DebugFunction]
        public static void Set(string propertyName, object value)
        {
            var prop = LineParser.properties.First(p => p.Key.PropertyExecutionName == propertyName);

            if (prop.Equals(default(KeyValuePair<DebugPropertyAttribute, PropertyInfo>)))
                throw new FieldAccessException("Property " + propertyName + " not found");

            if (!prop.Value.CanWrite)
                throw new FieldAccessException("Property " + propertyName + " is read only");
            var correctValue = Convert.ChangeType(value, prop.Value.PropertyType);
            prop.Value.SetValue(null, correctValue);
        }
    }
}
