using System.Collections.Generic;

namespace Common.Collections
{
	public interface IAppendableCollection<T>: IEnumerable<T>
	{
		void Add(T value);
		void Add(IEnumerable<T> values);
	}
}