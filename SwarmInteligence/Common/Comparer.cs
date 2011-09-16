using System;
using System.Collections.Generic;
using System.Diagnostics.Contracts;

namespace Common
{
	public class Comparer<T>: IComparer<T>
	{
		private readonly Func<T, T, int> comparer;

		public Comparer(Func<T, T, int> comparer)
		{
			Contract.Requires(comparer != null);
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