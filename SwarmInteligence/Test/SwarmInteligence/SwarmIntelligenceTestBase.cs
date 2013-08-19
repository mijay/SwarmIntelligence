using CommonTest;
using SILibrary;
using SILibrary.BuildUp;
using SILibrary.Empty;
using SILibrary.TwoDimensional;
using SwarmIntelligence;
using SwarmIntelligence.Core;
using SwarmIntelligence.Core.Loggin;
using SwarmIntelligence.Implementation.Playground;

namespace Test.SwarmInteligence
{
	public abstract class SwarmIntelligenceTestBase: TestBase
	{
		protected World<Coordinates2D, EmptyData, EmptyData> world;
		protected Runner<Coordinates2D, EmptyData, EmptyData> runner;
		protected ILogJournal journal;

		protected void InitializeWorld(Coordinates2D min, Coordinates2D max)
		{
			ILogManager logManager;
			world = SystemBuilder
				.Create<Coordinates2D, EmptyData, EmptyData>()
				.WithDefaultLog(out logManager)
				.WithTopology(new FourConnectedSurfaceTopology(min, max))
				.WithSurfaceMap()
				.WithEmptyNodeData()
				.WithEmptyEdgeData()
				.Build();
			journal = logManager.Journal;
			world = runner.World;
			runner = new Runner<Coordinates2D, EmptyData, EmptyData>(world);
		}
	}
}