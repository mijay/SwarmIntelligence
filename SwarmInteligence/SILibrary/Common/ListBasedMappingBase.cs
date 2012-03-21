using System.Collections.Generic;
using System.Linq;
using Common.Collections.Concurrent;
using Common.Collections.Extensions;
using SwarmIntelligence.Core.Loggin;
using SwarmIntelligence.MemoryManagement;

namespace SILibrary.Common
{
	public abstract class ListBasedMappingBase<TKey, TValue>: MappingBase<TKey, TValue>
		where TValue: class
	{
		private readonly ConcurrentList<TValue> values = new ConcurrentList<TValue>();

		protected ListBasedMappingBase(IValueProvider<TKey, TValue> valueProvider, ILog log)
			: base(valueProvider, log)
		{
		}

		protected abstract int ToIndex(TKey key);
		protected abstract TKey ToKey(int index);

		public override bool TryGet(TKey key, out TValue value)
		{
			value = values[ToIndex(key)];
			return value != null;
		}

		public override IEnumerator<KeyValuePair<TKey, TValue>> GetEnumerator()
		{
			return values
				.Select((value, index) => value == null
				                          	? (KeyValuePair<TKey, TValue>?) null
				                          	: new KeyValuePair<TKey, TValue>(ToKey(index), value))
				.NotNull()
				.GetEnumerator();
		}

		protected override bool TryRemove(TKey key, out TValue value)
		{
			var index = ToIndex(key);
			TValue tmpValue = values[index];
			if(tmpValue == null) {
				value = null;
				return false;
			}

			do {
				value = tmpValue;
				tmpValue = values.CompareExchange(index, null, value);
			} while(tmpValue != value);
			return value != null;
		}

		protected override TValue GetOrAdd(TKey key, TValue value)
		{
			var oldValue = values.CompareExchange(ToIndex(key), value, null);
			return oldValue ?? value;
		}
	}
}