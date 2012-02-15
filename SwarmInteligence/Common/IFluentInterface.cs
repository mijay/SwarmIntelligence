using System;
using System.ComponentModel;

namespace Common
{
	public interface IFluentInterface
	{
		[EditorBrowsable(EditorBrowsableState.Never)]
		string ToString();

		[EditorBrowsable(EditorBrowsableState.Never)]
		bool Equals(object other);

		[EditorBrowsable(EditorBrowsableState.Never)]
		int GetHashCode();

		[EditorBrowsable(EditorBrowsableState.Never)]
		Type GetType();
	}
}