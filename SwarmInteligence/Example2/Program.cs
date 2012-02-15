using System;
using SILibrary.Buildup;
using SILibrary.Empty;
using SILibrary.Graph;
using SwarmIntelligence;
using SwarmIntelligence.Core;
using SwarmIntelligence.Implementation.Logging;

namespace Example2
{
	internal class Programnew
	{
		private static World<GraphCoordinate, EmptyData, EdgeData> world;
		private static Runner<GraphCoordinate, EmptyData, EdgeData> runner;
		private static ILogJournal logger;

		public static void Main(string[] args)
		{
			Tuple<Runner<GraphCoordinate, EmptyData, EdgeData>, ILogJournal>
				tuple = SystemBuilder
					.Create<GraphCoordinate, EmptyData, EdgeData>()
					.WithTopology(new GraphTopology(null))
					.Build();

			runner = tuple.Item1;
			logger = tuple.Item2;
			world = runner.World;
		}
	}
}