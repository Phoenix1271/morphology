namespace Morphology.Conversion
{
    /// <summary>
    /// Supports the policy-driven construction of <see cref="IPropertyToken" />s for given object.
    /// </summary>
    public interface IPropertyConverter
    {
        #region Public Methods

        /// <summary>
        /// Converts supplied value into <see cref="IPropertyToken" />.
        /// </summary>
        /// <param name="value">Value to be converted to token.</param>
        /// <returns><see cref="IPropertyToken" /> for converted value.</returns>
        IPropertyToken Convert(object value);

        #endregion
    }
}
