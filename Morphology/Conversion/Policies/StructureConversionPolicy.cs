using System;
using System.Collections.Generic;
using System.Reflection;
using Morphology.Conversion.Tokens;
using Morphology.Extensions;

namespace Morphology.Conversion.Policies
{
    /// <summary>
    /// Determine if the supplied value is represented as Structure token.
    /// </summary>
    internal class StructureConversionPolicy : IConversionPolicy
    {
        #region IConversionPolicy

        /// <summary>
        /// If supported, convert the provided value into a <see cref="IPropertyToken"/>.
        /// </summary>
        /// <param name="converter">Converter for conversion of additional values.</param>
        /// <param name="value">The value to convert.</param>
        /// <param name="result">Value converted to <see cref="IPropertyToken"/> if conversion was successful.</param>
        /// <returns><c>true</c> if the value could be converted under this policy; <c>false</c> otherwise.</returns>
        public bool TryConvert(IPropertyConverter converter, object value, out IPropertyToken result)
        {
            result = null;

            if (converter == null) throw new ArgumentNullException(nameof(converter));
            if (value == null) return false;

            var type = value.GetType();
            string typeName = type.IsCompilerGenerated() ? null : type.Name;

            result = new StructureToken(GetProperties(value, converter), typeName);
            return true;
        }

        #endregion

        #region Private Methods

        private IEnumerable<IProperty> GetProperties(object value, IPropertyConverter converter)
        {
            foreach (var property in value.GetType().GetDerivedProperties())
            {
                object propertyValue;

                try
                {
                    propertyValue = property.GetValue(value);
                }
                catch (TargetParameterCountException)
                {
                    // These properties will be ignored since they never produce values they're not
                    // of concern to auditing and exceptions can be suppressed.
                    //TODO Add loging via debug looger: $"The property accessor {property} is a non-default indexer"
                    continue;
                }
                catch (TargetInvocationException ex)
                {
                    //TODO Add logging via debug logger
                    propertyValue =
                        $"The property accessor '{property.DeclaringType.FullName}.{property.Name}' threw an {ex.InnerException.GetType().Name}";
                }

                yield return new Property(property.Name, converter.Convert(propertyValue));
            }
        }

        #endregion
    }
}
