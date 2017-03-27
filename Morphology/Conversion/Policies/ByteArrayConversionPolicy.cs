using System;
using System.Linq;
using Morphology.Conversion.Tokens;

namespace Morphology.Conversion.Policies
{
    /// <summary>
    /// Determine if the supplied value is represented as Scalar token.
    /// </summary>
    public sealed class ByteArrayConversionPolicy : IConversionPolicy
    {
        #region Private Fields

        //TODO this should be somehow dynamically configurable
        private const int MaximumByteArrayLength = 1024;

        #endregion

        #region IConversionPolicy

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

            var bytes = value as byte[];
            if (bytes == null) return false;

            if (bytes.Length > MaximumByteArrayLength)
            {
                string hexValue = string.Concat(bytes.Take(16).Select(b => b.ToString("X2")));
                string description = $"0x: {hexValue}... ({bytes.Length} bytes)";
                result = new ScalarToken(description);
            }
            else
            {
                result = new ScalarToken(bytes.ToArray());
            }

            return true;
        }

        #endregion
    }
}
