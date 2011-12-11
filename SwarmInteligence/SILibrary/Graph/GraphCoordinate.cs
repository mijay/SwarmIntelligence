using SwarmIntelligence.Core.Space;

namespace SILibrary.Graph
{
	public struct GraphCoordinate: ICoordinate<GraphCoordinate>
	{
		public readonly int vertex;

		public GraphCoordinate(int vertex)
		{
			this.vertex = vertex;
		}

		#region ICoordinate<GraphCoordinate> Members

		public bool Equals(GraphCoordinate other)
		{
			return vertex == other.vertex;
		}

		public GraphCoordinate Clone()
		{
			return new GraphCoordinate(vertex);
		}

		public override int GetHashCode()
		{
			return vertex;
		}

		#endregion

		public override bool Equals(object obj)
		{
			return obj is GraphCoordinate && Equals((GraphCoordinate) obj);
		}

		public override string ToString()
		{
			return string.Format("{{{0}}}", vertex);
		}
	}
}