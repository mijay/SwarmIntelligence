using System;
using System.Collections.Generic;
using SILibrary.BuildUp;
using SILibrary.Empty;
using SILibrary.TwoDimensional;
using SwarmIntelligence;
using SwarmIntelligence.Core;
using SwarmIntelligence.Core.Loggin;
using SwarmIntelligence.Core.Playground;
using SwarmIntelligence.Implementation.Logging;
using SwarmIntelligence.Specialized;

namespace WpfApplication1
{
	public class Model
	{
		private readonly Coordinates2D max;
		private readonly Coordinates2D min;
		private readonly int preyCount;
		private readonly Random random = new Random();
		private readonly int wolfCount;
		private Runner<Coordinates2D, EmptyData, EmptyData> runner;
		private World<Coordinates2D, EmptyData, EmptyData> world;

		public Model(Coordinates2D min, Coordinates2D max, int wolfCount, int preyCount)
		{
			this.min = min;
			this.max = max;
			this.wolfCount = wolfCount;
			this.preyCount = preyCount;
		}

		private Coordinates2D GenerateCoordinates()
		{
			int x = random.Next(min.x, max.x + 1);
			int y = random.Next(min.y, max.y + 1);
			return new Coordinates2D(x, y);
		}

		private void SeedAnts(int count, bool isWolf)
		{
			using(IMapModifier<Coordinates2D, EmptyData, EmptyData> mapModifier = world.GetModifier())
				for(int i = 0; i < count; i++)
					mapModifier.AddAt(isWolf
					                  	? (IAnt<Coordinates2D, EmptyData, EmptyData>) new WolfAnt(world, 3)
					                  	: new PreyAnt(world, 3),
					                  GenerateCoordinates());
		}

		public void Initialize()
		{
			ILogManager logManager;
			world = SystemBuilder
				.Create<Coordinates2D, EmptyData, EmptyData>()
				.WithDefaultLog(out logManager)
				.WithTopology(new EightConnectedSurfaceTopology(min, max))
				.WithCommonMap()
				.WithEmptyNodeData()
				.WithEmptyEdgeData()
				.Build();
			ILogJournal logJournal = logManager.Journal;
			runner = new Runner<Coordinates2D, EmptyData, EmptyData>(world);

			logJournal.OnRecordsAdded += OnNewRecords;
			SeedAnts(wolfCount, true);
			SeedAnts(preyCount, false);
		}

		public void Turn()
		{
			runner.DoTurn();
		}

		public event Action<IEnumerable<LogRecord>> OnNewRecords;
	}
}