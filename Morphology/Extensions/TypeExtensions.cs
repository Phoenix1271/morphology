using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using Morphology.Conversion.Policies;

namespace Morphology.Extensions
{
    internal static class TypeExtensions
    {
        public static bool IsDictionary(this Type type)
        {
            if (type == null) throw new ArgumentNullException(nameof(type));

            return type.IsConstructedGenericType &&
                   type.GetGenericTypeDefinition() == typeof(Dictionary<,>) &&
                   IsValidDictionaryKeyType(type.GenericTypeArguments[0]);
        }

        private static bool IsValidDictionaryKeyType(Type type)
        {
            return ScalarConversionPolicy.BuildInTypes.Contains(type) || type.GetTypeInfo().IsEnum;
        }
    }
}