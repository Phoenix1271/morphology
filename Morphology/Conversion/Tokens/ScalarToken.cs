namespace Morphology.Conversion.Tokens
{
    /// <summary>
    /// A token representing a simple scalar type.
    /// </summary>
    public class ScalarToken : IPropertyToken
    {
        #region Constructors

        /// <summary>
        /// Construct a <see cref="ScalarToken"/> with the specified value.
        /// </summary>
        /// <param name="value">The value, which may be <see langword="null"/>.</param>
        public ScalarToken(object value)
        {
            Value = value;
        }

        #endregion

        #region Public Properties

        /// <summary>
        /// The value, which may be <see langword="null"/>.
        /// </summary>
        public object Value { get; }

        #endregion
    }
}
