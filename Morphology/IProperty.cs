namespace Morphology
{
    /// <summary>
    /// Represents one property with token containing it's value
    /// </summary>
    public interface IProperty
    {
        #region Public Properties

        /// <summary>
        /// The name of the property.
        /// </summary>
        string Name { get; }

        /// <summary>
        /// Token associated with the property.
        /// </summary>
        IPropertyToken Token { get; }

        #endregion
    }
}
