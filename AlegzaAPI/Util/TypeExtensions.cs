using System;
using System.Collections.Generic;
using System.Linq;

namespace AlegzaCRM.AlegzaAPI.Util
{
    internal static class TypeExtensions
    {
        internal static bool IsCollectionType(this Type type)
        {
            if (!type.GetGenericArguments().Any()) return false;

            Type genericTypeDefinition = type.GetGenericTypeDefinition();
            var collectionTypes = new[] { typeof(IEnumerable<>), typeof(ICollection<>), typeof(IList<>), typeof(List<>) };
            return collectionTypes.Any(x => x.IsAssignableFrom(genericTypeDefinition));
        }
    }
}
