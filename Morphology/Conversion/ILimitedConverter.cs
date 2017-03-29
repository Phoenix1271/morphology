using Morphology.Conversion.Tokens;

namespace Morphology.Conversion
{
    /// <summary>
    /// Supports the policy-driven construction of <see cref="IPropertyToken"/>s
    /// for given object with destructuring depth limited to certain level.
    /// </summary>
    public interface ILimitedConverter : IPropertyConverter
    {
        #region Public Methods

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
        IPropertyToken Convert(object value, int depthLimit);

        #endregion
    }
}
