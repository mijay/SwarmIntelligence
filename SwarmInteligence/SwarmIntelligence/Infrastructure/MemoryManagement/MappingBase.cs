using System.Collections;
using System.Collections.Generic;
using SwarmIntelligence.Core;
using SwarmIntelligence.Core.Interfaces;
using SwarmIntelligence.Infrastructure.Logging;

namespace SwarmIntelligence.Infrastructure.MemoryManagement
{
	public abstract class MappingBase<TKey, TValue>: ISparsedMapping<TKey, TValue>, ICompleteMapping<TKey, TValue>
	{
		private readonly ILog log;
		private readonly IValueProvider<TKey, TValue> valueProvider;

		protected MappingBase(IValueProvider<TKey, TValue> valueProvider, ILog log)
		{
			this.valueProvider = valueProvider;
			this.log = log;
		}

		#region ICompleteMapping<TKey,TValue> Members

		public TValue Get(TKey key)
		{
			TValue value;
			if(TryGet(key, out value))
				return value;

			TValue createdCell = valueProvider.Get(key);
			value = GetOrAdd(key, createdCell);
			if(createdCell.Equals(value))
				valueProvider.Return(createdCell);

			log.Log(CommonLogTypes.CellBuilded, key);
			return value;
		}

		#endregion

		#region ISparsedMapping<TKey,TValue> Members

		public abstract bool TryGet(TKey key, out TValue value);

		public abstract IEnumerator<KeyValuePair<TKey, TValue>> GetEnumerator();

		IEnumerator IEnumerable.GetEnumerator()
		{
			return GetEnumerator();
		}

		#endregion

		public void Free(TKey coordinate)
		{
			TValue value;
			if(!TryRemove(coordinate, out value))
				return;
			valueProvider.Return(value);

			log.Log(CommonLogTypes.CellFreed, coordinate);
		}

		protected abstract bool TryRemove(TKey key, out TValue value);
		protected abstract TValue GetOrAdd(TKey key, TValue value);
	}
}