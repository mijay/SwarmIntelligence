using System;
using System.Diagnostics.Contracts;
using SwarmIntelligence.Infrastructure.MemoryManagement;

namespace SILibrary.MemoryManagement
{
	public class DummyValueProvider<TKey, TValue>: IValueProvider<TKey, TValue>
	{
		private readonly Func<TKey, TValue> builder;

		public DummyValueProvider(Func<TKey, TValue> builder)
		{
			Contract.Requires(builder != null);
			this.builder = builder;
		}

		#region Implementation of IValueProvider<in TKey,TValue>

		public void Return(TValue cell)
		{
			if(cell is IDisposable)
				((IDisposable) cell).Dispose();
		}

		public TValue Get(TKey key)
		{
			return builder(key);
		}

		#endregion
	}
}