using SILibrary.Common;
using SwarmIntelligence.Core.Loggin;
using SwarmIntelligence.MemoryManagement;

namespace SILibrary.Graph
{
	public class GraphMapping<TValue>: ListBasedMappingBase<GraphCoordinate, TValue>
		where TValue: class
	{
		public GraphMapping(IValueProvider<GraphCoordinate, TValue> valueProvider, ILog log)
			: base(valueProvider, log)
		{
		}

		#region Overrides of ListBasedMappingBase<GraphCoordinate,TValue>

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