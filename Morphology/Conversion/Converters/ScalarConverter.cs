using System;
using Morphology.Configuration;
using Morphology.Conversion.Policies;
using Morphology.Conversion.Tokens;

namespace Morphology.Conversion.Converters
{
    /// <summary>
    /// Converts all types to scalar strings.
    /// </summary>
    internal class ScalarConverter : IPropertyConverter
    {
        #region Private Fields

        private static readonly IConversionPolicy NullPolicy = new NullConversionPolicy();
        private readonly ILogger _logger;
        private readonly StringConversionPolicy _scalarPolicy;

        #endregion

        #region Constructors

        /// <summary>
        /// Creates a new <see cref="ScalarConverter"/>
        /// </summary>
        /// <param name="config">Configuration for property conversion.</param>
        /// <param name="logger">Logger for logging conversion errors.</param>
        public ScalarConverter(IConversionConfig config, ILogger logger)
        {
            _logger = logger;
            _scalarPolicy = new StringConversionPolicy(config);
        }

        #endregion

        #region IPropertyConverter

        /// <summary>
        /// Converts supplied value into <see cref="IPropertyToken"/>.
        /// </summary>
        /// <param name="value">Value to be converted to token.</param>
        /// <returns><see cref="IPropertyToken"/> for converted value.</returns>
        public IPropertyToken Convert(object value)
        {
            IPropertyToken result;
            if (NullPolicy.TryConvert(this, value, out result)) return result;

            string text = value.ToString();

            try
            {
                if (_scalarPolicy.TryConvert(this, value.ToString(), out result)) return result;
            }
            catch (Exception ex)
            {
                _logger.Error(ex,
                    $"Exception caught when using policy '{_scalarPolicy.GetType().FullName}' for property conversion of '{value}'.");
            }

            return new ScalarToken(text);
        }

        #endregion
    }
}
