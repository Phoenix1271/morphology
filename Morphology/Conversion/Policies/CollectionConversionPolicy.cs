using System;
using System.Collections;
using System.Linq;
using Morphology.Conversion.Tokens;
using Morphology.Extensions;

namespace Morphology.Conversion.Policies
{
    /// <summary>
    /// Determine if the supplied value is represented as Sequence of property tokens.
    /// </summary>
    internal sealed class CollectionConversionPolicy : IConversionPolicy
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

            var enumerable = value as IEnumerable;
            if (enumerable == null) return false;
            if (value.GetType().IsDictionary()) return false;

            var elements = enumerable.Cast<object>()
                //.Take() TODO restrict number of elements from configuration
                .Select(converter.Convert);

            result = new SequenceToken(elements);
            return true;
        }

        #endregion
    }
}
