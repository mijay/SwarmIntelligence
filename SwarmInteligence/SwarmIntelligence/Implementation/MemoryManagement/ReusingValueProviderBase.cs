using System.Collections.Concurrent;
using SwarmIntelligence.MemoryManagement;

namespace SwarmIntelligence.Implementation.MemoryManagement
{
	public abstract class ReusingValueProviderBase<TKey, TValue>: IValueProvider<TKey, TValue>
	{
		//todo: может сделать не наследование а делегирование?
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
			return Create(key);
		}

		#endregion

		protected abstract TValue Create(TKey key);
		protected abstract void Modify(TValue value, TKey newKey);
	}
}