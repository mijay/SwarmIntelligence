using System;
using System.Collections.Generic;
using System.Diagnostics.Contracts;
using System.Linq;

namespace Utils
{
    public static class EnumerableExtension
    {
        public static bool AreDistinct<T>(this IEnumerable<T> source)
        {
            Contract.Requires<ArgumentNullException>(source != null);
            return source.Distinct().Count() == source.Count();
        }
    }
}