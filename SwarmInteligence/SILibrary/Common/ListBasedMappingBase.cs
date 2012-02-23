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
			TValue tmpValue = values[ToIndex(key)];
			if(tmpValue == null) {
				value = null;
				return false;
			}

			do {
				value = tmpValue;
				tmpValue = values.CompareExchange(ToIndex(key), null, value);
			} while(tmpValue != value);
			return value != null;
		}

		protected override TValue GetOrAdd(TKey key, TValue value)
		{
			return values.CompareExchange(ToIndex(key), value, null);
		}
	}
}