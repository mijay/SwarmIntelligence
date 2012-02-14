using System;
using SILibrary.General;
using SILibrary.General.Background;
using SILibrary.Graph;
using SwarmIntelligence;
using SwarmIntelligence.Core;
using SwarmIntelligence.Core.Space;
using SwarmIntelligence.Infrastructure.Logging;

namespace Example2
{
	internal class Programnew
	{
		private static World<GraphCoordinate, EmptyData, DictionaryDataLayer<Edge<GraphCoordinate>, TupleDataEdge>> world;
		private static Runner<GraphCoordinate, EmptyData, DictionaryDataLayer<Edge<GraphCoordinate>, TupleDataEdge>> runner;
		private static ILogJournal logger;

		public static void Main(string[] args)
		{
			Tuple<Runner<GraphCoordinate, EmptyData, DictionaryDataLayer<Edge<GraphCoordinate>, TupleDataEdge>>, ILogJournal>
				tuple = SystemBuilder
					.Create<GraphCoordinate, EmptyData, DictionaryDataLayer<Edge<GraphCoordinate>, TupleDataEdge>>()
					.WithTopology(new GraphTopology(null))
					.Build();

			runner = tuple.Item1;
			logger = tuple.Item2;
			world = runner.World;
		}
	}
}