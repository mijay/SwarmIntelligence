using SILibrary.Buildup;
using SILibrary.Empty;
using SILibrary.Graph;
using SwarmIntelligence;
using SwarmIntelligence.Core;
using SwarmIntelligence.Core.Loggin;

namespace Example2
{
	internal class Programnew
	{
		private static World<GraphCoordinate, EmptyData, EdgeData> world;
		private static Runner<GraphCoordinate, EmptyData, EdgeData> runner;
		private static ILogJournal logger;

		public static void Main(string[] args)
		{
			ILogManager logManager;
			world = SystemBuilder
				.Create<GraphCoordinate, EmptyData, EdgeData>()
				.WithDefaultLog(out logManager)
				.WithTopology(new GraphTopology(null))
				.WithCommonMap()
				.WithEmptyNodeData()
				.WithEmptyEdgeData()
				.Build();

			runner = new Runner<GraphCoordinate, EmptyData, EdgeData>(world);
			logger = logManager.Journal;
		}
	}
}