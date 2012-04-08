using System.Collections;
using System.Collections.Generic;
using System.Diagnostics.Contracts;
using Common;
using SwarmIntelligence.Core.Interfaces;

namespace SwarmIntelligence.Contracts
{
	[ContractClassFor(typeof(ISparsedMapping<,>))]
	public abstract class ISparsedMappingContract<TKey, TValue>: ISparsedMapping<TKey, TValue>
	{
		#region ISparsedMapping<TKey,TValue> Members

		public bool TryGet(TKey coordinate, out TValue value)
		{
			Contract.Ensures(!Contract.Result<bool>() || !Contract.ValueAtReturn(out value).Equals(default(TValue)));
			throw new UnreachableCodeException();
		}

		public IEnumerator<KeyValuePair<TKey, TValue>> GetEnumerator()
		{
			throw new UnreachableCodeException();
		}

		IEnumerator IEnumerable.GetEnumerator()
		{
			return GetEnumerator();
		}

		#endregion
	}
}