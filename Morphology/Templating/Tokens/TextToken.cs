using System.Text.RegularExpressions;

namespace Morphology.Templating.Tokens
{
    /// <summary>
    /// Represents text token in the text template.
    /// </summary>
    /// <seealso cref="Morphology.Templating.ITemplateToken" />
    public sealed class TextToken : ITemplateToken
    {
        #region Constructors

        /// <summary>
        /// Initializes a new instance of the <see cref="TextToken"/> class.
        /// </summary>
        /// <param name="match">The RegEx match for text token.</param>
        internal TextToken(Match match)
        {
            RawValue = match.Value;
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="TextToken"/> class.
        /// </summary>
        /// <param name="rawValue">The raw value of text token.</param>
        internal TextToken(string rawValue)
        {
            RawValue = rawValue ?? string.Empty;
        }

        #endregion

        #region ITemplateToken

        /// <summary>
        /// Gets raw value of the token.
        /// </summary>
        public string RawValue { get; }

        #endregion
    }
}
