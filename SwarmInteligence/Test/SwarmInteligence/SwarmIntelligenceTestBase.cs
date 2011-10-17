using CommonTest;
using SILibrary.Common;
using SILibrary.General.Background;
using SILibrary.TwoDimensional;
using SwarmIntelligence;
using SwarmIntelligence.Core;
using SwarmIntelligence.Core.Space;
using SwarmIntelligence.Infrastructure.GrabgeCollection;
using SwarmIntelligence.Infrastructure.Logging;

namespace Test.SwarmInteligence
{
	public abstract class SwarmIntelligenceTestBase: TestBase
	{
		protected World<Coordinates2D, EmptyData, EmptyData> world;
		protected Runner<Coordinates2D, EmptyData, EmptyData> runner;
		protected LogManager logger;

		protected void InitializeWorld(Coordinates2D min, Coordinates2D max)
		{
			logger = new LogManager();
			var topology = new FourConnectedSurfaceTopology(min, max);
			CellProvider<Coordinates2D, EmptyData, EmptyData> cellProvider = SetCell<Coordinates2D, EmptyData, EmptyData>.Provider();
			var map = new DictionaryMap<Coordinates2D, EmptyData, EmptyData>(topology, cellProvider, logger.Log);
			var nodeDataLayer = new EmptyDataLayer<Coordinates2D>();
			var edgeDataLayer = new EmptyDataLayer<Edge<Coordinates2D>>();
			world = new World<Coordinates2D, EmptyData, EmptyData>(nodeDataLayer, edgeDataLayer, map, logger.Log);
			runner = new Runner<Coordinates2D, EmptyData, EmptyData>(world, new GarbageCollector<Coordinates2D, EmptyData, EmptyData>());
		}
	}
}