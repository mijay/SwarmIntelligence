using System.Collections.Concurrent;
using SwarmIntelligence.Infrastructure.MemoryManagement;

namespace SILibrary.MemoryManagement
{
	public abstract class ReusingValueProviderBase<TKey, TValue>: IValueProvider<TKey, TValue>
	{
		private readonly ConcurrentBag<TValue> bag = new ConcurrentBag<TValue>();

		#region Overrides of IValueProvider<TKey, TValue>

		public void Return(TValue cellBase)
		{
			bag.Add(cellBase);
		}

		public TValue Get(TKey key)
		{
			TValue result;
			if(bag.TryTake(out result)) {
				Modify(result, key);
				return result;
			}
			return Create();
		}

		#endregion

		protected abstract TValue Create();
		protected abstract void Modify(TValue value, TKey newKey);
	}
}