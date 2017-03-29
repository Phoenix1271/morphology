namespace Morphology.Conversion
{
    /// <summary>
    /// Determine how a supplied value is represented as a complex property.
    /// </summary>
    public interface IConversionPolicy
    {
        #region Public Methods

        /// <summary>
        /// If supported, convert the provided value into a <see cref="IPropertyToken"/>.
        /// </summary>
        /// <param name="converter">Converter for conversion of additional values.</param>
        /// <param name="value">The value to convert.</param>
        /// <param name="result">Value converted to <see cref="IPropertyToken"/> if conversion was successful.</param>
        /// <returns><c>true</c> if the value could be converted under this policy; <c>false</c> otherwise.</returns>
        bool TryConvert(IPropertyConverter converter, object value, out IPropertyToken result);

        #endregion
    }
}
