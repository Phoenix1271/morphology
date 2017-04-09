using System;
using Morphology.Conversion.Tokens;

namespace Morphology.Conversion.Converters
{
    /// <summary>
    /// Limits destructuring of the object to certain depth.
    /// </summary>
    internal class LimitedConverter : ILimitedConverter
    {
        #region Private Fields

        private readonly ILimitedConverter _converter;
        private readonly int _depthLimit;

        #endregion

        #region Constructors

        /// <summary>
        /// Construct a <see cref="LimitedConverter"/> with the specified converter and conversion limit.
        /// </summary>
        /// <param name="converter">Converter to be limited.</param>
        /// <param name="depthLimit">
        /// Limits the depth to which conversion looks for a properties of input vallue />
        /// </param>
        /// <exception cref="ArgumentNullException"><paramref name="converter"/> is <see langword="null"/>.</exception>
        public LimitedConverter(ILimitedConverter converter, int depthLimit)
        {
            _converter = converter ?? throw new ArgumentNullException(nameof(converter));

            if (depthLimit < 0) depthLimit = 0;
            _depthLimit = depthLimit;
        }

        #endregion

        #region ILimitedConverter

        /// <summary>
        /// Converts supplied value into <see cref="IPropertyToken"/>.
        /// </summary>
        /// <param name="value">Value to be converted to token.</param>
        /// <returns><see cref="IPropertyToken"/> for converted value.</returns>
        public IPropertyToken Convert(object value)
        {
            return Convert(value, _depthLimit);
        }

        /// <summary>
        /// Converts supplied value into <see cref="IPropertyToken"/>.
        /// </summary>
        /// <param name="value">Value to be converted to token.</param>
        /// <param name="depthLimit">
        /// Limits destructuring of the <see cref="value"/> to given depth.
        /// If the depth is exceeded conversion will return <see cref="ScalarToken"/>
        /// with <see langword="null"/> null value.
        /// </param>
        /// <returns><see cref="IPropertyToken"/> for converted value.</returns>
        public IPropertyToken Convert(object value, int depthLimit)
        {
            return depthLimit >= 0 ? _converter.Convert(value, depthLimit - 1) : new ScalarToken(null);
        }

        #endregion
    }
}
