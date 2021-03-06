﻿using System;
using System.Collections.Generic;
using System.Linq;
using Morphology.Formatting;

namespace Morphology.Conversion.Tokens
{
    /// <summary>
    /// A token representing an ordered sequence of values.
    /// </summary>
    public sealed class SequenceToken : IPropertyToken
    {
        #region Constructors

        /// <summary>
        /// Create a <see cref="SequenceToken"/> with the provided <paramref name="elements"/>.
        /// </summary>
        /// <param name="elements">The elements of the sequence.</param>
        public SequenceToken(IEnumerable<IPropertyToken> elements)
        {
            Elements = (elements ?? Enumerable.Empty<IPropertyToken>()).ToArray();
        }

        #endregion

        #region Public Properties

        /// <summary>
        /// The elements of the sequence.
        /// </summary>
        public IReadOnlyList<IPropertyToken> Elements { get; }

        #endregion

        #region IPropertyToken

        /// <summary>
        /// Renders content of property token to specified format.
        /// </summary>
        /// <param name="formatter">Formater used to format token's content.</param>
        public void Render(IPropertyFormatter formatter)
        {
            if (formatter == null) throw new ArgumentNullException(nameof(formatter));

            formatter.Format(this);
        }

        #endregion
    }
}
