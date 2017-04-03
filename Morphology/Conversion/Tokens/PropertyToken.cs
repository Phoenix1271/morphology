using System;
using Morphology.Formatting;

namespace Morphology.Conversion.Tokens
{
    /// <summary>
    /// A token representing property with it's value.
    /// </summary>
    public class PropertyToken : IPropertyToken
    {
        #region Constructors

        /// <summary>
        /// Create a <see cref="PropertyToken"/> with the provided name and value.
        /// </summary>
        /// <param name="name">The name of the property.</param>
        /// <param name="value">The value of the property.</param>
        internal PropertyToken(string name, IPropertyToken value)
        {
            if (string.IsNullOrEmpty(name)) throw new ArgumentNullException(nameof(name));

            Name = name;
            Value = value ?? throw new ArgumentNullException(nameof(value));
        }

        #endregion

        #region Public Properties

        /// <summary>
        /// The name of the property.
        /// </summary>
        public string Name { get; }

        /// <summary>
        /// Value associated with the property.
        /// </summary>
        public IPropertyToken Value { get; }

        #endregion

        #region IPropertyToken

        /// <summary>
        /// Renders content of property to specified format.
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
