using SILibrary.Common;

namespace SILibrary.Graph
{
	public class GraphValueStorage<TValue>: ListValueStorageBase<GraphCoordinate, TValue>
		where TValue: class
	{
		#region Overrides of ListValueStorageBase<GraphCoordinate,TValue>

		protected override int ToIndex(GraphCoordinate key)
		{
			return key.vertex;
		}

		protected override GraphCoordinate ToKey(int index)
		{
			return new GraphCoordinate(index);
		}

		#endregion
	}
}