using System.Collections.Generic;

namespace Common.Collections
{
	public interface IAppendableCollection<T>: ITailableCollection<T>
	{
		void Append(T value);
		void Append(IEnumerable<T> values);
	}
}