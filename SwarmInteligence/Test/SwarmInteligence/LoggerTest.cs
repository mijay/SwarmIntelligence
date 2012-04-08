using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using NUnit.Framework;
using SILibrary.Empty;
using SILibrary.TwoDimensional;
using SwarmIntelligence;
using SwarmIntelligence.Core;
using SwarmIntelligence.Implementation.Logging;
using SwarmIntelligence.Implementation.Playground;
using SwarmIntelligence.Specialized;

namespace Test.SwarmInteligence
{
	public class LoggerTest: SwarmIntelligenceTestBase
	{
		#region Setup/Teardown

		public override void SetUp()
		{
			base.SetUp();
			InitializeWorld(new Coordinates2D(0, 0), new Coordinates2D(100, 100));
			number = 0;
		}

		#endregion

		private static int number;

		private class TestAnt: AntBase<Coordinates2D, EmptyData, EmptyData>
		{
			private readonly Random r = new Random();

			public TestAnt(World<Coordinates2D, EmptyData, EmptyData> world)
				: base(world)
			{
			}

			protected override void DoProcessTurn()
			{
				int x = r.Next(0, 101);
				int y = r.Next(0, 101);

				Log.Log("test", Interlocked.Increment(ref number));

				this.MoveTo(new Coordinates2D(x, y));
			}
		}

		[Test]
		public void SimpleTest()
		{
			var logRecords = new List<LogRecord>();
			journal.OnRecordsAdded +=
				newRecords => {
					lock(logRecords) {
						logRecords.AddRange(newRecords.Where(x => x.type == "test"));
					}
				};

			using(IMapModifier<Coordinates2D, EmptyData, EmptyData> modifier = world.GetModifier())
				modifier.AddAt(new TestAnt(world), new Coordinates2D(1, 1));

			for(int i = 0; i < 1000; i++)
				runner.DoTurn();
			Thread.Sleep(200); // тк добавление в лог асинхронное и отложенное

			CollectionAssert.AreEqual(Enumerable.Range(1, 1000).ToArray(),
			                          logRecords.Select(x => (int) x.arguments[0]).ToArray());
		}
	}
}