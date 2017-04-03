using System;
using System.Linq;
using Morphology.Configuration;
using Morphology.Conversion.Tokens;

namespace Morphology.Conversion.Policies
{
    /// <summary>
    /// Determine if the supplied value is represented as Scalar token.
    /// </summary>
    internal sealed class ByteArrayConversionPolicy : IConversionPolicy
    {
        #region Private Fields

        private readonly IConversionConfig _config;

        #endregion

        #region Constructors

        /// <summary>
        /// Creates a new <see cref="ByteArrayConversionPolicy"/>
        /// </summary>
        /// <param name="config">Configuration for property conversion.</param>
        public ByteArrayConversionPolicy(IConversionConfig config)
        {
            _config = config ?? throw new ArgumentNullException(nameof(config));
        }

        #endregion

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

            var bytes = value as byte[];
            if (bytes == null) return false;

            //Enforce limit on size of array
            if (_config.ByteArrayLimit > 0 && bytes.Length > _config.ByteArrayLimit)
            {
                string hexValue = string.Concat(bytes.Take(16).Select(b => b.ToString("X2")));
                string description = $"0x: {hexValue}... ({bytes.Length} bytes)";
                result = new ScalarToken(description);
                return true;
            }

            result = new ScalarToken(bytes.ToArray());
            return true;
        }

        #endregion
    }
}
