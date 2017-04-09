using Morphology.Configuration;
using Morphology.Conversion.Converters;

namespace Morphology.Conversion
{
    /// <summary>
    /// Factory that provides <see cref="IPropertyConverter"/>s.
    /// </summary>
    internal class PropertyConverterFactory
    {
        #region Private Fields

        private readonly IConversionConfig _config;
        private readonly ILogger _logger;

        #endregion

        #region Constructors

        /// <summary>
        /// Creates a new <see cref="PropertyConverterFactory"/>.
        /// </summary>
        /// <param name="config">Configuration for property conversion.</param>
        /// <param name="logger">Logger for logging conversion errors.</param>
        public PropertyConverterFactory(IConversionConfig config, ILogger logger)
        {
            _config = config;
            _logger = logger;
        }

        #endregion

        #region Public Methods

        /// <summary>
        /// Creates a new <see cref="IPropertyConverter"/> based on conversion type
        /// </summary>
        /// <param name="type">Type of conversion. If not specified configured value is used.</param>
        /// <returns>Instance of <see cref="IPropertyConverter"/>.</returns>
        public IPropertyConverter Create(ConversionType? type)
        {
            if (type == null) type = _config.ConversionType;

            switch (type)
            {
                case ConversionType.Stringify:
                    return new ScalarConverter(_config, _logger);
                default:
                    return new StructuralConverter(_config, _logger);
            }
        }

        #endregion
    }
}
