﻿using System;
using System.Reflection;
using Morphology.Conversion.Tokens;

namespace Morphology.Conversion.Policies
{
    /// <summary>
    /// Determine if the supplied value is represented as Scalar token.
    /// </summary>
    internal sealed class EnumConversionPolicy : IConversionPolicy
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
            if (!value.GetType().GetTypeInfo().IsEnum) return false;

            result = new ScalarToken(value);
            return true;
        }

        #endregion
    }
}
