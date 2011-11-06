using System;
using System.Collections.Generic;
using SILibrary.General;
using SILibrary.General.Background;
using SILibrary.TwoDimensional;
using SwarmIntelligence;
using SwarmIntelligence.Core;
using SwarmIntelligence.Core.Playground;
using SwarmIntelligence.Infrastructure.Logging;
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
					                  	? (IAnt<Coordinates2D, EmptyData, EmptyData>) new WolfAnt(world)
					                  	: new PreyAnt(world),
					                  GenerateCoordinates());
		}

		public void Initialize()
		{
			ILogJournal logJournal;
			runner = SystemBuilder
				.Create<Coordinates2D, EmptyData, EmptyData>()
				.WithTopology(new EightConnectedSurfaceTopology(min, max))
				.Build(out logJournal);
			logJournal.OnRecordsAdded += OnNewRecords;
			world = runner.World;

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