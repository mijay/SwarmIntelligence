using System.Collections.Generic;
using System.Diagnostics.Contracts;

namespace Common.Collections
{
    public static class CollectionExtensions
    {
        public static ICollection<T> AsReadonly<T>(this ICollection<T> collection)
        {
            Contract.Requires(collection != null);
            return new ReadOnlyCollection<T>(collection);
        }
    }
}