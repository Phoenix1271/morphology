using Morphology.Formatting;

namespace Morphology.Conversion
{
    /// <summary>
    /// A token representing some property of the object.
    /// </summary>
    public interface IPropertyToken
    {
        #region Public Methods

        /// <summary>
        /// Renders content of property token to specified format.
        /// </summary>
        /// <param name="formatter">Formater used to format token's content.</param>
        void Render(IPropertyFormatter formatter);

        #endregion
    }
}
