namespace Morphology.Conversion
{
    /// <summary>
    /// Supports the policy-driven construction of <see cref="IPropertyToken" />s
    /// for given object with destructuring depth limited to certain level.
    /// </summary>
    public interface ILimitedConverter : IPropertyConverter
    {
        /// <summary>
        /// Converts supplied value into <see cref="IPropertyToken" />.
        /// </summary>
        /// <param name="value">Value to be converted to token.</param>
        /// <param name="limit">Maximum limit for structure conversion.</param>
        /// <returns><see cref="IPropertyToken" /> for converted value.</returns>
        IPropertyToken Convert(object value, int limit);
    }
}