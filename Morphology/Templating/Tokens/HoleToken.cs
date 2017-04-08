using System.Text.RegularExpressions;
using Morphology.Conversion;

namespace Morphology.Templating.Tokens
{
    /// <summary>
    /// Token representing a hole in the text template.
    /// </summary>
    /// <seealso cref="Morphology.Templating.ITemplateToken"/>
    internal sealed class HoleToken : ITemplateToken
    {
        #region Constructors

        /// <summary>
        /// Initializes a new instance of the <see cref="HoleToken"/> class.
        /// </summary>
        /// <param name="match">The RegEx match for hole token.</param>
        public HoleToken(Match match)
        {
            RawValue = match.Value;

            string name = match.Groups["name"].Value.Trim();
            Name = !string.IsNullOrWhiteSpace(name) ? name : null;

            string format = match.Groups["format"].Value.Trim();
            Format = !string.IsNullOrWhiteSpace(format) ? format : null;

            int indexValue;
            string index = match.Groups["index"].Value.Trim();
            Index = int.TryParse(index, out indexValue) ? (int?) indexValue : null;

            string hint = match.Groups["hint"].Value.Trim();
            switch (hint)
            {
                case "$":
                    ConversionHint = ConversionHint.String;
                    break;
                case "@":
                    ConversionHint = ConversionHint.Structure;
                    break;
                default:
                    ConversionHint = ConversionHint.Default;
                    break;
            }

            int alignmentValue;
            string alignment = match.Groups["alignment"].Value.Trim();
            Alignment = int.TryParse(alignment, out alignmentValue) ? alignmentValue : 0;
        }

        #endregion

        #region Public Properties

        /// <summary>
        /// Gets text alignment for text literal.
        /// </summary>
        public int Alignment { get; }

        /// <summary>
        /// Gets the conversion hint for object serialization.
        /// </summary>
        public ConversionHint ConversionHint { get; }

        /// <summary>
        /// Gets the format for text literal.
        /// </summary>
        public string Format { get; }

        /// <summary>
        /// Gets the index of sbustitute property.
        /// </summary>
        public int? Index { get; }

        /// <summary>
        /// Gets the name of the substitute property.
        /// </summary>
        public string Name { get; }

        #endregion

        #region ITemplateToken

        /// <summary>
        /// Gets raw value of the token.
        /// </summary>
        public string RawValue { get; }

        #endregion
    }
}
