using System.Collections.Generic;

namespace Common.Collections
{
	public static class CollectionExtensions
	{
		public static ICollection<T> AsReadonly<T>(this ICollection<T> collection)
		{
			Requires.NotNull(collection);
			return new ReadOnlyCollection<T>(collection);
		}
	}
}