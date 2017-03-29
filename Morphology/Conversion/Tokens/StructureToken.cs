using System.Collections.Generic;
using System.Linq;

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
        public StructureToken(IEnumerable<IProperty> properties, string typeName = null)
        {
            Properties = (properties ?? Enumerable.Empty<IProperty>()).ToArray();
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
        public IReadOnlyList<IProperty> Properties { get; }

        /// <summary>
        /// A piece of metadata describing the "type" of the
        /// structure, or <see langword="null"/>.
        /// </summary>
        public string TypeName { get; }

        #endregion
    }
}
