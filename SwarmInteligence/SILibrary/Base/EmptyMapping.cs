using System.Collections.Generic;
using System.Linq;
using SILibrary.MemoryManagement;
using SwarmIntelligence.Infrastructure.Logging;
using SwarmIntelligence.Infrastructure.MemoryManagement;

namespace SILibrary.Base
{
	public class EmptyMapping<TKey>: MappingBase<TKey, EmptyData>
	{
		public EmptyMapping()
			: base(new DummyValueProvider<TKey, EmptyData>(_ => new EmptyData()), EmptyLogger.Instanse)
		{
		}

		#region Overrides of MappingBase<TKey,TValue>

		public override bool TryGet(TKey key, out EmptyData value)
		{
			value = default(EmptyData);
			return false;
		}

		public override IEnumerator<KeyValuePair<TKey, EmptyData>> GetEnumerator()
		{
			return Enumerable.Empty<KeyValuePair<TKey, EmptyData>>().GetEnumerator();
		}

		protected override bool TryRemove(TKey key, out EmptyData value)
		{
			value = default(EmptyData);
			return false;
		}

		protected override EmptyData GetOrAdd(TKey key, EmptyData value)
		{
			return value;
		}

		#endregion
	}
}