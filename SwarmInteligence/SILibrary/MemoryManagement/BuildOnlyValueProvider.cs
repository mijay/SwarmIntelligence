using System;
using System.Diagnostics.Contracts;
using SwarmIntelligence.Infrastructure.MemoryManagement;

namespace SILibrary.MemoryManagement
{
	public class BuildOnlyValueProvider<TKey, TValue>: IValueProvider<TKey, TValue>
	{
		private readonly Func<TKey, TValue> builder;

		public BuildOnlyValueProvider(Func<TKey, TValue> builder)
		{
			Contract.Requires(builder != null);
			this.builder = builder;
		}

		#region Implementation of IValueProvider<in TKey,TValue>

		public void Return(TValue cell)
		{
			throw new NotSupportedException();
		}

		public TValue Get(TKey key)
		{
			return builder(key);
		}

		#endregion
	}
}