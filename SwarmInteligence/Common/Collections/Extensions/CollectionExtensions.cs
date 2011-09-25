using System.Collections.Generic;
using System.Diagnostics.Contracts;
using Common.Collections.ReadOnly;

namespace Common.Collections.Extensions
{
	public static class CollectionExtensions
	{
		[Pure]
		public static ICollection<T> AsReadonly<T>(this ICollection<T> collection)
		{
			Contract.Requires(collection != null);
			return new ReadOnlyCollection<T>(collection);
		}
	}
}