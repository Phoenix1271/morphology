using Morphology.Formatting;

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

        #region Public Methods

        /// <summary>
        /// Renders content of property token to specified format.
        /// </summary>
        /// <param name="formatter">Formater used to format token's content.</param>
        void Render(IPropertyFormatter formatter);

        #endregion
    }
}
