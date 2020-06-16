namespace DebugConsole
{
    using System;

    /// <summary>
    ///   A set of predefined <see cref="DebugFunctionAttribute"/>s
    /// </summary>
    public static class PredefinedFunctions
    {
        /// <summary>
        ///   Overrides the old value of a static property with a new one.
        /// </summary>
        /// <param name="propertyName">
        ///   The property to be set.
        /// </param>
        /// <param name="value">
        ///   The new value of the given property.
        /// </param>
        /// <exception cref="FieldAccessException">
        ///   The property is readonly or the property cannot be found.
        /// </exception>
        public static void Set(string propertyName, object value)
        {
            (var prop, var mayWrite) = LineParser.GetProperty(propertyName);

            if (!mayWrite)
                throw new FieldAccessException("Property " + propertyName + " is readonly");

            var correctValue = Convert.ChangeType(value, prop.PropertyType);
            prop.SetValue(null, correctValue);
        }

        /// <summary>
        ///   Gets the value of a static property.
        /// </summary>
        /// <param name="propertyName">
        ///   The property to be read.
        /// </param>
        /// <returns>
        ///   The value of the property.
        /// </returns>
        /// <exception cref="FieldAccessException">
        ///   The property is writeonly or cannot be found.
        /// </exception>
        public static object Get(string propertyName)
        {
            (var prop, var mayWrite) = LineParser.GetProperty(propertyName);

            if (!prop.CanRead)
                throw new FieldAccessException("Property " + propertyName + " is writeonly");

            return prop.GetValue(null);
        }
    }
}
