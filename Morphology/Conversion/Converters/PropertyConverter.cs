using System;
using System.Collections.Generic;
using Morphology.Conversion.Policies;
using Morphology.Conversion.Tokens;

namespace Morphology.Conversion.Converters
{
    /// <summary>
    /// Destructurizes given object to <see cref="IPropertyToken"/>.
    /// </summary>
    public class PropertyConverter : ILimitedConverter
    {
        #region Private Fields

        private static readonly IEnumerable<IConversionPolicy> BuildInPolicies = new IConversionPolicy[]
        {
            new ScalarConversionPolicy(),
            new EnumConversionPolicy(),
            new ByteArrayConversionPolicy(),
            new DelegateConversionPolicy(),
            new ReflectionTypeConversionPolicy(),
            new DictionaryConversionPolicy(),
            new CollectionConversionPolicy(),
            new StructureConversionPolicy()
        };

        private static readonly IConversionPolicy NullPolicy = new NullConversionPolicy();

        #endregion

        #region IPropertyConverter

        /// <summary>
        /// Converts supplied value into <see cref="IPropertyToken" />.
        /// </summary>
        /// <param name="value">Value to be converted to token.</param>
        /// <returns><see cref="IPropertyToken" /> for converted value.</returns>
        public IPropertyToken Convert(object value)
        {
            //TODO make this dynamically configurable
            return Convert(value, 5);
        }

        /// <summary>
        /// Converts supplied value into <see cref="IPropertyToken" />.
        /// </summary>
        /// <param name="value">Value to be converted to token.</param>
        /// <param name="limit">Maximum limit for structure conversion.</param>
        /// <returns><see cref="IPropertyToken" /> for converted value.</returns>
        public IPropertyToken Convert(object value, int limit)
        {
            //TODO set this to configurable limit if there is invalid value
            if (limit < 0) limit = 1;

            IPropertyToken result;
            if (NullPolicy.TryConvert(this, value, out result)) return result;

            var limiter = new LimitedConverter(this, limit);
            foreach (var policy in BuildInPolicies)
            {
                try
                {
                    if (policy.TryConvert(limiter, value, out result)) return result;
                }
                catch (Exception)
                {
                    //TODO this should be logged
                }
            }

            //Fallback in case that there will be no policy to take care of value
            return new ScalarToken(value.ToString());
        }

        #endregion
    }
}
