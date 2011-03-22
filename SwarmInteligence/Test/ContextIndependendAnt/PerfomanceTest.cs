using System;
using NUnit.Framework;
using Utils;

namespace Test.ContextIndependendAnt
{
    public class PerfomanceTest: TestBase
    {
        public static void RunTest(int minX, int minY, int maxX, int maxY, int ants, int jumps)
        {
            var test = new Test(minX, minY, maxX, maxY);
            int size = (maxY - minY) * (maxX - minX);
            test.SetUp();
            Console.Write(string.Format("size - {0}; ", size));
            test.SimpleTest(jumps, ants);
        }

        [Test]
        [Ignore]
        public void RunAll()
        {
            var sizes = new[]
                        {
                            Tuple.Create(-5, -5, 10, 10),
                            Tuple.Create(-100, -300, 400, 800)
                        };

            var tests = new[]
                        {
                            Tuple.Create(sizes[0], 30, 10),
                            Tuple.Create(sizes[0], 400, 10),
                            Tuple.Create(sizes[0], 30, 1000),
                            Tuple.Create(sizes[0], 400, 1000),
                            Tuple.Create(sizes[1], 300, 100),
                            Tuple.Create(sizes[1], 4000, 100),
                            Tuple.Create(sizes[1], 300, 10000),
                            //Tuple.Create(sizes[1], 40000, 10000),
                        };

            tests.ForEach(x => RunTest(x.Item1.Item1, x.Item1.Item2, x.Item1.Item3, x.Item1.Item4, x.Item2, x.Item3));
        }
    }
}