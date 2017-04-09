using Morphology.Conversion.Tokens;

namespace Morphology.Templating.Tokens
{
    /// <summary>
    /// Represents token that is bound with given property
    /// </summary>
    /// <seealso cref="Morphology.Templating.ITemplateToken" />
    public class BoundToken : ITemplateToken
    {
        #region Constructors

        /// <summary>
        /// Initializes a new instance of the <see cref="BoundToken"/> class.
        /// </summary>
        /// <param name="hole">The hole that is being bound.</param>
        /// <param name="property">The property that is bound to given hole.</param>
        internal BoundToken(HoleToken hole, PropertyToken property)
        {
            Property = property;
            RawValue = hole.RawValue;
            Alignment = hole.Alignment;
            Format = hole.Format;
        }

        #endregion

        #region Public Properties

        /// <summary>
        /// Gets text alignment for text literal.
        /// </summary>
        public int Alignment { get; }

        /// <summary>
        /// Gets the format for text literal.
        /// </summary>
        public string Format { get; }

        /// <summary>
        /// Property to be used for text literal substitution.
        /// </summary>
        public PropertyToken Property { get; }

        #endregion

        #region ITemplateToken

        /// <summary>
        /// Gets raw value of the token.
        /// </summary>
        public string RawValue { get; }

        #endregion
    }
}
