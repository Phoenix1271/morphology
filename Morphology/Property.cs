using System;

namespace Morphology
{
    /// <summary>
    /// A property model with it's value
    /// </summary>
    public class Property : IProperty
    {
        #region Constructors

        internal Property(string name, IPropertyToken token)
        {
            Name = name ?? throw new ArgumentNullException(nameof(name));
            Token = token ?? throw new ArgumentNullException(nameof(token));
        }

        #endregion

        #region IProperty

        /// <summary>
        /// The name of the property.
        /// </summary>
        public string Name { get; }

        /// <summary>
        /// Token associated with the property.
        /// </summary>
        public IPropertyToken Token { get; }

        #endregion
    }
}
