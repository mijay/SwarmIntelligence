using System;
using SILibrary.General;
using SILibrary.General.Background;
using SILibrary.Graph;
using SwarmIntelligence;
using SwarmIntelligence.Core;
using SwarmIntelligence.Infrastructure.Logging;

namespace Example2
{
	internal class Program
	{
		private static World<GraphCoordinate, OdorData, EmptyData> world;
		private static Runner<GraphCoordinate, OdorData, EmptyData> runner;
		private static ILogJournal logger;

		private static void Main(string[] args)
		{
			Tuple<Runner<GraphCoordinate, OdorData, EmptyData>, ILogJournal> tuple = SystemBuilder
				.Create<GraphCoordinate, OdorData, EmptyData>()
				.WithTopology(new GraphTopology(null))
				.Build();

			logger = tuple.Item2;
			runner = tuple.Item1;
			world = tuple.Item1.World;
		}
	}
}