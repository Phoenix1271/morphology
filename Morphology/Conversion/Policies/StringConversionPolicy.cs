using System;
using Morphology.Configuration;
using Morphology.Conversion.Tokens;

namespace Morphology.Conversion.Policies
{
    /// <summary>
    /// Determine if the supplied value is represented as Scalar token.
    /// </summary>
    public class StringConversionPolicy : IConversionPolicy
    {
        #region Private Fields

        private readonly IConversionConfig _config;

        #endregion

        #region Constructors

        /// <summary>
        /// Creates a new <see cref="StringConversionPolicy"/>
        /// </summary>
        /// <param name="config">Configuration for property conversion.</param>
        public StringConversionPolicy(IConversionConfig config)
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

            string text = value as string;
            if (text == null) return false;

            if (_config.StringLimit > 0 && text.Length > _config.StringLimit)
            {
                text = text.Substring(0, _config.StringLimit) + "…";
            }

            result = new ScalarToken(text);
            return true;
        }

        #endregion
    }
}
