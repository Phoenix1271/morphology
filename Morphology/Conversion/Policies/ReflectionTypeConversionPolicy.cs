using System;
using System.Reflection;
using Morphology.Conversion.Tokens;

namespace Morphology.Conversion.Policies
{
    /// <summary>
    /// Determine if the supplied value is represented as Scalar value.
    /// </summary>
    internal sealed class ReflectionTypeConversionPolicy : IConversionPolicy
    {
        #region IConversionPolicy

        /// <summary>
        /// If supported, convert the provided value into a <see cref="IPropertyToken"/>.
        /// </summary>
        /// <param name="converter">Converter for conversion of additional values.</param>
        /// <param name="value">The value to convert.</param>
        /// <param name="result">Value converted to <see cref="IPropertyToken"/> if conversion was successful.</param>
        /// <returns><c>true</c> if the value could be converted under this policy; <c>false</c> otherwise.</returns>
        public bool TryConvert(IPropertyConverter converter, object value, out IPropertyToken result)
        {
            result = null;

            if (converter == null) throw new ArgumentNullException(nameof(converter));

            // These types and their subclasses are property-laden and deep
            // Most of targets will convert them to strings
            if (!(value is Type) && !(value is MemberInfo)) return false;

            result = new ScalarToken(value.ToString());
            return true;
        }

        #endregion
    }
}
