﻿using System;
using System.Collections.Generic;
using System.Linq;
using Morphology.Formatting;

namespace Morphology.Conversion.Tokens
{
    /// <summary>
    /// A token representing a collection of name value properties.
    /// </summary>
    public sealed class StructureToken : IPropertyToken
    {
        #region Constructors

        /// <summary>
        /// Construct a <see cref="StructureToken"/> with the provided properties.
        /// </summary>
        /// <param name="properties">The properties of the structure.</param>
        /// <param name="typeName">
        /// Optionally, a piece of metadata describing the "type" of the structure.
        /// </param>
        public StructureToken(IEnumerable<PropertyToken> properties, string typeName = null)
        {
            Properties = (properties ?? Enumerable.Empty<PropertyToken>()).ToArray();
            TypeName = typeName;
        }

        #endregion

        #region Public Properties

        /// <summary>
        /// The properties of the structure.
        /// </summary>
        /// <remarks>
        /// Not presented as a dictionary because dictionary construction is
        /// relatively expensive; it is cheaper to build a dictionary over properties
        /// only when the structure is of interest.
        /// </remarks>
        public IReadOnlyList<PropertyToken> Properties { get; }

        /// <summary>
        /// A piece of metadata describing the "type" of the
        /// structure, or <see langword="null"/>.
        /// </summary>
        public string TypeName { get; }

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
