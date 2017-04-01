using Morphology.Conversion.Tokens;

namespace Morphology.Formatting
{
    /// <summary>
    /// Provides formatting to <see cref="IPropertyToken"/>s
    /// </summary>
    public interface IPropertyFormatter
    {
        /// <summary>
        /// Formats content of <see cref="ScalarToken"/>.
        /// </summary>
        /// <param name="token">Token to be formatted.</param>
        void Format(ScalarToken token);

        /// <summary>
        /// Formats content of <see cref="DictionaryToken"/>.
        /// </summary>
        /// <param name="token">Token to be formatted.</param>
        void Format(DictionaryToken token);

        /// <summary>
        /// Formats content of <see cref="SequenceToken"/>.
        /// </summary>
        /// <param name="token">Token to be formatted.</param>
        void Format(SequenceToken token);

        /// <summary>
        /// Formats content of <see cref="StructureToken"/>.
        /// </summary>
        /// <param name="token">Token to be formatted.</param>
        void Format(StructureToken token);

        /// <summary>
        /// Formats content of <see cref="StructureToken"/>.
        /// </summary>
        /// <param name="property">Property to be formatted.</param>
        void Format(IProperty property);
    }
}