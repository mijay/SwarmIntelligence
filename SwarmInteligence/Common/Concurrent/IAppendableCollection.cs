using System.Collections.Generic;
using System.Diagnostics.Contracts;
using Common.Contracts;

namespace Common.Concurrent
{
	[ContractClass(typeof(IAppendableCollectionContract<>))]
	public interface IAppendableCollection<T>: IEnumerable<T>
	{
		T this[long index] { get; }
		long Count { get; }
		void Append(T value);
		void Append(IEnumerable<T> values);
		IEnumerable<T> ReadFrom(long index);
	}
}