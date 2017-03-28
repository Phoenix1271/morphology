using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using Morphology.Conversion.Policies;

namespace Morphology.Extensions
{
    internal static class TypeExtensions
    {
        #region Public Methods

        public static IEnumerable<PropertyInfo> GetDerivedProperties(this Type type)
        {
            if (type == null) throw new ArgumentNullException(nameof(type));

            var visited = new HashSet<string>();
            TypeInfo typeInfo = type.GetTypeInfo();

            while (typeInfo.AsType() != typeof(object))
            {
                var notVisited = typeInfo.DeclaredProperties
                    .Where(p => !visited.Contains(p.Name) && p.CanRead &&
                                p.GetMethod.IsPublic && !p.GetMethod.IsStatic &&
                                (p.Name != "Item" || p.GetIndexParameters().Length == 0));

                foreach (PropertyInfo propertyInfo in notVisited)
                {
                    visited.Add(propertyInfo.Name);
                    yield return propertyInfo;
                }

                typeInfo = typeInfo.BaseType.GetTypeInfo();
            }
        }

        public static bool IsCompilerGenerated(this Type type)
        {
            if (type == null) throw new ArgumentNullException(nameof(type));

            TypeInfo typeInfo = type.GetTypeInfo();
            string typeName = type.Name;

            //C# Anonymous types always start with "<>" and VB's start with "VB$"
            return typeInfo.IsGenericType &&
                   typeInfo.IsSealed &&
                   typeInfo.IsNotPublic &&
                   type.Namespace == null &&
                   (typeName.StartsWith("<>") || typeName.StartsWith("VB$"));
        }

        public static bool IsDictionary(this Type type)
        {
            if (type == null) throw new ArgumentNullException(nameof(type));

            return type.IsConstructedGenericType &&
                   type.GetGenericTypeDefinition() == typeof(Dictionary<,>) &&
                   IsValidDictionaryKeyType(type.GenericTypeArguments[0]);
        }

        #endregion

        #region Private Methods

        private static bool IsValidDictionaryKeyType(Type type)
        {
            return ScalarConversionPolicy.BuildInTypes.Contains(type) || type.GetTypeInfo().IsEnum;
        }

        #endregion
    }
}
