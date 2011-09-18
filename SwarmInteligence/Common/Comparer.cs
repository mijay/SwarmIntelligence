using System;
using System.Collections.Generic;

namespace Common
{
	public class Comparer<T>: IComparer<T>
	{
		private readonly Func<T, T, int> comparer;

		public Comparer(Func<T, T, int> comparer)
		{
			Requires.NotNull(comparer);
			this.comparer = comparer;
		}

		#region Implementation of IComparer<in T>

		public int Compare(T x, T y)
		{
			return comparer(x, y);
		}

		#endregion
	}
}