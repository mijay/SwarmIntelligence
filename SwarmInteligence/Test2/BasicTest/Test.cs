using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using NUnit.Framework;
using SwarmIntelligence2.Core;
using SwarmIntelligence2.Core.Commands;
using SwarmIntelligence2.Core.Coordinates;
using SwarmIntelligence2.GeneralImplementation;
using SwarmIntelligence2.GeneralImplementation.Background;
using SwarmIntelligence2.TwoDimensional;
using Utils;

namespace Test2.BasicTest
{
    public class Test: TestBase
    {
        #region Setup/Teardown

        public override void SetUp()
        {
            base.SetUp();

            size = new Range<Coordinates2D>(new Coordinates2D(-4, -3), new Coordinates2D(12, 5));
            map = new DictionaryMap<Coordinates2D, EmptyData>(size);
            background = new EmptyBackground<Coordinates2D>(size);

            runner = new Runner<Coordinates2D, EmptyData>(map, background, () => new CommandEvaluator<Coordinates2D, EmptyData>());
        }

        #endregion

        public override void FixtureSetUp()
        {
            base.FixtureSetUp();
            RangeValidator2D.Register();
            random = new Random();
        }

        private Range<Coordinates2D> size;
        private DictionaryMap<Coordinates2D, EmptyData> map;
        private EmptyBackground<Coordinates2D> background;
        private Random random;
        private Runner<Coordinates2D, EmptyData> runner;

        private IEnumerable<TestAnt> SeedAnts(int antsNumber, int timesAntJumps, params Coordinates2D[] lastAntSteps)
        {
            return EnumerableExtension
                .Repeat(() => SeedAnt(timesAntJumps, lastAntSteps), antsNumber)
                .ToArray();
        }

        private TestAnt SeedAnt(int timesAntJumps, params Coordinates2D[] lastAntSteps)
        {
            Coordinates2D initialPosition = GenerateCoordinate();
            var testAnt = new TestAnt(EnumerableExtension
                                          .Repeat(GenerateCoordinate, timesAntJumps)
                                          .Concat(lastAntSteps)
                                          .ToArray());
            map[initialPosition].Add(testAnt);
            return testAnt;
        }

        private Coordinates2D GenerateCoordinate()
        {
            int x = random.Next(size.min.x, size.max.x + 1);
            int y = random.Next(size.min.y, size.max.y + 1);
            return new Coordinates2D(x, y);
        }

        [Test]
        public void SimpleTest([Values(5, 10, 20, 80, 200)] int jumps, [Values(100, 1000)] int ants)
        {
            Coordinates2D lastStep = GenerateCoordinate();
            IEnumerable<TestAnt> seededAnts = SeedAnts(ants, jumps - 1, lastStep);
            var timer = new Stopwatch();
            timer.Start();
            for(int i = 0; i < jumps; ++i)
                runner.ProcessTurn();
            timer.Stop();
            Debug.WriteLine(string.Format("jumps - {0}; ants - {1}; time - {2}", jumps, ants, timer.ElapsedMilliseconds));

            KeyValuePair<Coordinates2D, Cell<Coordinates2D, EmptyData>> keyValuePair = map.GetExistenData().Single();
            Assert.That(keyValuePair.Key, Is.EqualTo(lastStep));
            CollectionAssert.AreEquivalent(seededAnts, keyValuePair.Value);
        }
    }
}