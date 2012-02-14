using System.Collections;
using System.Collections.Generic;
using System.Diagnostics.Contracts;
using Common;
using SwarmIntelligence.Core;

namespace SwarmIntelligence.Contracts
{
	[ContractClassFor(typeof(IMapping<,>))]
	public abstract class IMappingContract<TKey, TValue>: IMapping<TKey, TValue>
	{
		#region IMapping<TKey,TValue> Members

		public bool TryGet(TKey coordinate, out TValue value)
		{
			Contract.Ensures(Contract.Result<bool>() ? !Contract.ValueAtReturn(out value).Equals(default(TValue)) : true);
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