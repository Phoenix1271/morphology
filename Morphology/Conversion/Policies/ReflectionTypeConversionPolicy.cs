using System;
using System.Reflection;
using Morphology.Conversion.Tokens;

namespace Morphology.Conversion.Policies
{
    public sealed class ReflectionTypeConversionPolicy : IConversionPolicy
    {
        public bool TryConvert(IPropertyConverter converter, object value, out IPropertyToken result)
        {
            result = null;

            if (converter == null) throw new ArgumentNullException(nameof(converter));

            // These types and their subclasses are property-laden and deep
            // Most of targets will convert them to strings
            if (!(value is Type) && !(value is MemberInfo)) return false;

            result = new ScalarToken(value.ToString());
            return true;
        }
    }
}