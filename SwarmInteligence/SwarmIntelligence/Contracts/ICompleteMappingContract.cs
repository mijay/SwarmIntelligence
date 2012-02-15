using System.Collections;
using System.Collections.Generic;
using System.Diagnostics.Contracts;
using Common;
using SwarmIntelligence.Core.Interfaces;

namespace SwarmIntelligence.Contracts
{
	[ContractClassFor(typeof(ICompleteMapping<,>))]
	public abstract class ICompleteMappingContract<TKey, TValue> : ICompleteMapping<TKey, TValue>
	{
		#region ICompleteMapping<TKey,TValue> Members

		public TValue Get(TKey key)
		{
			Contract.Ensures(!Contract.Result<TValue>().Equals(default(TValue)));
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