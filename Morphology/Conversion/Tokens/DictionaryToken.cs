using System.Collections.Generic;
using System.Linq;

namespace Morphology.Conversion.Tokens
{
    /// <summary>
    /// A token representing a dictionary type.
    /// </summary>
    public sealed class DictionaryToken : IPropertyToken
    {
        #region Constructors

        /// <summary>
        /// Create a <see cref="DictionaryToken" /> with the provided <paramref name="elements" />.
        /// </summary>
        /// <param name="elements">The elements of the dictionary.</param>
        public DictionaryToken(IEnumerable<KeyValuePair<ScalarToken, IPropertyToken>> elements)
        {
            Elements = (elements ?? Enumerable.Empty<KeyValuePair<ScalarToken, IPropertyToken>>())
                .ToDictionary(kvp => kvp.Key, kvp => kvp.Value);
        }

        #endregion

        #region Public Properties

        /// <summary>
        /// The elements of the sequence.
        /// </summary>
        public IReadOnlyDictionary<ScalarToken, IPropertyToken> Elements { get; }

        #endregion
    }
}
