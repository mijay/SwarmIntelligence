using SwarmIntelligence.Core.Space;

namespace SILibrary.Buildup
{
	public static class SystemBuilder
	{
		public static ILogConfiguration<TCoordinate, TNodeData, TEdgeData> Create<TCoordinate, TNodeData, TEdgeData>()
			where TCoordinate: ICoordinate<TCoordinate>
		{
			return new InitialConfiguration<TCoordinate, TNodeData, TEdgeData>(new BuildingWorld<TCoordinate, TNodeData, TEdgeData>());
		}
	}
}