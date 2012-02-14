using System.Collections.Generic;
using System.Linq;
using SILibrary.MemoryManagement;
using SwarmIntelligence.Core;
using SwarmIntelligence.Infrastructure.MemoryManagement;

namespace SILibrary.Base
{
	public class EmptyMapping<TKey, TValue>: MappingBase<TKey, TValue>
	{
		public EmptyMapping(DummyValueProvider<TKey, TValue> valueProvider, ILog log)
			: base(valueProvider, log)
		{
		}

		#region Overrides of MappingBase<TKey,TValue>

		public override bool TryGet(TKey key, out TValue value)
		{
			value = default(TValue);
			return false;
		}

		public override IEnumerator<KeyValuePair<TKey, TValue>> GetEnumerator()
		{
			return Enumerable.Empty<KeyValuePair<TKey, TValue>>().GetEnumerator();
		}

		protected override bool TryRemove(TKey key, out TValue value)
		{
			value = default(TValue);
			return false;
		}

		protected override TValue GetOrAdd(TKey key, TValue value)
		{
			return value;
		}

		#endregion
	}
}