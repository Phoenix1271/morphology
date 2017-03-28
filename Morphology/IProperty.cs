namespace Morphology
{
    /// <summary>
    /// A property model with it's value
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
