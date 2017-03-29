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

        private static readonly IConversionPolicy NullPolicy = new NullConversionPolicy();
        private static readonly IEnumerable<IConversionPolicy> StandardPolicies = new IConversionPolicy[]
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

        #endregion

        #region ILimitedConverter

        /// <summary>
        /// Converts supplied value into <see cref="IPropertyToken"/>.
        /// </summary>
        /// <param name="value">Value to be converted to token.</param>
        /// <returns><see cref="IPropertyToken"/> for converted value.</returns>
        public IPropertyToken Convert(object value)
        {
            //TODO make this dynamically configurable
            return Convert(value, 5);
        }

        /// <summary>
        /// Converts supplied value into <see cref="IPropertyToken"/>
        /// </summary>
        /// <param name="value">Value to be converted to token.</param>
        /// <param name="depthLimit">
        /// Limits destructuring of the <see cref="value"/> to given depth.
        /// If the depth is exceeded conversion will return <see cref="ScalarToken"/>
        /// with <see langword="null"/> null value.
        /// </param>
        public IPropertyToken Convert(object value, int depthLimit)
        {
            if (depthLimit < 0) return new ScalarToken(null);

            IPropertyToken result;
            if (NullPolicy.TryConvert(this, value, out result)) return result;

            var limiter = new LimitedConverter(this, depthLimit);
            foreach (var policy in StandardPolicies)
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
