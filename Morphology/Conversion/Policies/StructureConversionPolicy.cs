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
        #region Private Fields

        private readonly ILogger _logger;

        #endregion

        #region Constructors

        public StructureConversionPolicy(ILogger logger)
        {
            _logger = logger;
        }

        #endregion

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

        private IEnumerable<PropertyToken> GetProperties(object value, IPropertyConverter converter)
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
                    _logger.Warning(
                        $"The property accessor '{property.DeclaringType.FullName}.{property.Name}' is a non-default indexer");

                    continue;
                }
                catch (TargetInvocationException ex)
                {
                    _logger.Error(ex,
                        $"The property accessor '{property.DeclaringType.FullName}.{property.Name}' threw an {ex.InnerException.GetType().Name}");

                    propertyValue = $"The property accessor threw an exception: {ex.InnerException.GetType().Name}";
                }

                yield return new PropertyToken(property.Name, converter.Convert(propertyValue));
            }
        }

        #endregion
    }
}
