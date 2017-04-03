using System;
using System.Collections;
using System.Linq;
using Morphology.Configuration;
using Morphology.Conversion.Tokens;
using Morphology.Extensions;

namespace Morphology.Conversion.Policies
{
    /// <summary>
    /// Determine if the supplied value is represented as Sequence of property tokens.
    /// </summary>
    internal sealed class CollectionConversionPolicy : IConversionPolicy
    {
        #region Private Fields

        private readonly IConversionConfig _config;

        #endregion

        #region Constructors

        /// <summary>
        /// Creates a new <see cref="ByteArrayConversionPolicy"/>
        /// </summary>
        /// <param name="config">Configuration for property conversion.</param>
        public CollectionConversionPolicy(IConversionConfig config)
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
            if (value.GetType().IsDictionary()) return false;

            var elements = enumerable.Cast<object>();
            if (_config.ItemLimit > 0)
            {
                elements = elements.Take(_config.ItemLimit);
            }

            result = new SequenceToken(elements.Select(converter.Convert));
            return true;
        }

        #endregion
    }
}
