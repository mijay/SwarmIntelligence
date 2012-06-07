using System;
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
			Contract.Requires(!ReferenceEquals(key, null));
			Contract.Ensures(!ReferenceEquals(Contract.Result<TValue>(), null));
			throw new UnreachableCodeException();
		}

		public void Set(TKey key, TValue value)
		{
			Contract.Requires(!ReferenceEquals(key, null));
			Contract.Requires(!ReferenceEquals(value, null));
			throw new UnreachableCodeException();
		}

		#endregion
	}
}