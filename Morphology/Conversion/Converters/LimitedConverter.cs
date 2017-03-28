using System;
using Morphology.Conversion.Tokens;

namespace Morphology.Conversion.Converters
{
    /// <summary>
    /// Limits destructuring of the object to certain depth.
    /// </summary>
    public class LimitedConverter : ILimitedConverter
    {
        #region Private Fields

        private readonly ILimitedConverter _converter;
        private readonly int _limit;

        #endregion

        #region Constructors

        /// <summary>
        /// Construct a <see cref="LimitedConverter"/> with the specified converter and conversion limit.
        /// </summary>
        /// <param name="converter">Converter to be limited.</param>
        /// <param name="limit">Maximum limit for structure conversion.</param>
        /// <exception cref="ArgumentNullException"><paramref name="converter"/> is <see langword="null"/>.</exception>
        public LimitedConverter(ILimitedConverter converter, int limit)
        {
            _converter = converter ?? throw new ArgumentNullException(nameof(converter));
            _limit = limit >= 0 ? limit : 5; //TODO set default value
        }

        #endregion

        #region ILimitedConverter

        /// <summary>
        /// Converts supplied value into <see cref="IPropertyToken" />.
        /// </summary>
        /// <param name="value">Value to be converted to token.</param>
        /// <returns><see cref="IPropertyToken" /> for converted value.</returns>
        public IPropertyToken Convert(object value)
        {
            return _limit >= 0 ? Convert(value, _limit -1) : new ScalarToken(null);
        }

        /// <summary>
        /// Converts supplied value into <see cref="IPropertyToken" />.
        /// </summary>
        /// <param name="value">Value to be converted to token.</param>
        /// <param name="limit">Maximum limit for structure conversion.</param>
        /// <returns><see cref="IPropertyToken" /> for converted value.</returns>
        public IPropertyToken Convert(object value, int limit)
        {
            return limit >= 0 ? _converter.Convert(value, limit) : new ScalarToken(null);
        }

        #endregion
    }
}
