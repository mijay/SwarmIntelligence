using System.Collections.Generic;
using System.Diagnostics.Contracts;
using Common.Collections.ReadOnly;

namespace Common.Collections.Extensions
{
	public static class ListExtension
	{
		/// <summary>
		/// Add an <paramref name="elem"/> to <paramref name="list"/> at <paramref name="index"/>.
		/// Fill <paramref name="list"/> with default elements of type <typeparamref name="T"/>
		/// if it is necessary to achieve <paramref name="index"/>.
		/// </summary>
		public static void SetAt<T>(this IList<T> list, int index, T elem)
		{
			Contract.Requires(list != null && index >= 0);
			if(list.Count > index)
				list[index] = elem;
			else {
				for(int i = index - list.Count; i > 0; --i)
					list.Add(default(T));
				list.Add(elem);
			}
		}

		/// <summary>
		/// Build read-only <see cref="IList{T}"/> from data given in <paramref name="list"/>.
		/// </summary>
		[Pure]
		public static IList<T> AsReadOnly<T>(this IList<T> list)
		{
			Contract.Requires(list != null);
			return new ReadOnlyList<T>(list);
		}
	}
}