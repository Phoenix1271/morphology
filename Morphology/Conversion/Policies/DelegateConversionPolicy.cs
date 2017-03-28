using System;
using System.Reflection;
using Morphology.Conversion.Tokens;

namespace Morphology.Conversion.Policies
{
    /// <summary>
    /// Determine if value can be represented as Scalar token
    /// </summary>
    public sealed class DelegateConversionPolicy : IConversionPolicy
    {
        /// <summary>
        /// If supported, convert the provided value into a <see cref="IPropertyToken" />.
        /// </summary>
        /// <param name="converter">Converter for conversion of additional values.</param>
        /// <param name="value">The value to convert.</param>
        /// <param name="result">Value converted to <see cref="IPropertyToken" /> if conversion was successful.</param>
        /// <returns><c>true</c> if the value could be converted under this policy; <c>false</c> otherwise.</returns>
        public bool TryConvert(IPropertyConverter converter, object value, out IPropertyToken result)
        {
            result = null;

            if (converter == null) throw new ArgumentNullException(nameof(converter));

            var del = value as Delegate;
            if (del == null) return false;

            result = new ScalarToken(del.GetMethodInfo().ToString());
            return true;
        }
    }
}