using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using SwarmIntelligence.Core.Interfaces;

namespace SILibrary.Empty
{
	public class EmptyMapping<TKey>: ICompleteMapping<TKey, EmptyData>, ISparsedMapping<TKey, EmptyData>
	{
		#region ICompleteMapping<TKey,EmptyData> Members

		public EmptyData Get(TKey key)
		{
			return new EmptyData();
		}

		public void Set(TKey key, EmptyData value)
		{
			throw new NotSupportedException();
		}

		public IEnumerator<KeyValuePair<TKey, EmptyData>> GetEnumerator()
		{
			return Enumerable.Empty<KeyValuePair<TKey, EmptyData>>().GetEnumerator();
		}

		IEnumerator IEnumerable.GetEnumerator()
		{
			return GetEnumerator();
		}

		#endregion

		#region ISparsedMapping<TKey,EmptyData> Members

		public bool TryGet(TKey key, out EmptyData value)
		{
			value = default(EmptyData);
			return false;
		}

		#endregion
	}
}