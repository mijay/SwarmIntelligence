using System.Collections.Generic;
using System.Diagnostics.Contracts;
using Common.Contracts;

namespace Common.Collections
{
	[ContractClass(typeof(ITailableCollectionContract<>))]
	public interface ITailableCollection<T>: IEnumerable<T>
	{
		T this[long index] { get; }
		long Count { get; }
		IEnumerable<T> ReadFrom(long index);
	}
}