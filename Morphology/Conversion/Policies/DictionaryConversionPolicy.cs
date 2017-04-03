using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using Morphology.Configuration;
using Morphology.Conversion.Tokens;
using Morphology.Extensions;

namespace Morphology.Conversion.Policies
{
    /// <summary>
    /// Determine if value can be represented as Dictionary token
    /// </summary>
    internal class DictionaryConversionPolicy : IConversionPolicy
    {
        #region Private Fields

        private readonly IConversionConfig _config;

        #endregion

        #region Constructors

        /// <summary>
        /// Creates a new <see cref="DictionaryConversionPolicy"/>
        /// </summary>
        /// <param name="config">Configuration for property conversion.</param>
        public DictionaryConversionPolicy(IConversionConfig config)
        {
            _config = config ?? throw new ArgumentNullException(nameof(config));
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

            var enumerable = value as IEnumerable;
            if (enumerable == null) return false;

            // Only dictionaries with 'scalar' keys are permitted, as
            // more complex keys may not serialize to unique values for
            // representation in targets. This check strengthens the expectation
            // that resulting dictionary is representable in JSON as well
            // as richer formats (e.g. XML, .NET type-aware...).
            // Only actual dictionaries are supported, as arbitrary types
            // can implement multiple IDictionary interfaces and thus introduce
            // multiple different interpretations.
            var type = value.GetType();
            if (!type.IsDictionary()) return false;

            var typeInfo = typeof(KeyValuePair<,>).MakeGenericType(type.GenericTypeArguments).GetTypeInfo();
            var keyProperty = typeInfo.GetDeclaredProperty("Key");
            var valueProperty = typeInfo.GetDeclaredProperty("Value");

            var items = enumerable.Cast<object>();
            if (_config.ItemLimit > 0)
            {
                items = items.Take(_config.ItemLimit);
            }

            var elements = items
                .Select(kvp =>
                    new KeyValuePair<ScalarToken, IPropertyToken>(
                        (ScalarToken) converter.Convert(keyProperty.GetValue(kvp)),
                        converter.Convert(valueProperty.GetValue(kvp))
                    )
                )
                .Where(kvp => kvp.Key.Value != null);

            result = new DictionaryToken(elements);
            return true;
        }

        #endregion
    }
}
