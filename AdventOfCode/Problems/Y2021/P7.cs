using CoreAoC.Entities;
using System.Runtime.CompilerServices;

[assembly: InternalsVisibleTo("TestingProject")]
namespace AdventOfCode.Problems.Y2021
{
    internal class P7 : Problem
    {
        internal class P7_1 : Part
        {
            protected override object Compute(IEnumerable<string> lines)
                => ComputeNonRecursive(lines);

            private static int ComputeNonRecursive(IEnumerable<string> lines)
            {
                IEnumerable<int> values = lines.Single().Split(',').Select(int.Parse);

                return SumCost(values, Median(values));
            }

            private static int SumCost(IEnumerable<int> values, int median)
                => values.Sum(n => Math.Abs(median - n));

            private static int Median(IEnumerable<int> values)
                => values.OrderByDescending(i => i)
                    .Select((v, i) => (v, i))
                    .Single(t => t.i == values.Count() / 2).v;
        }

        internal class P7_2 : Part
        {
            protected override object Compute(IEnumerable<string> lines)
                => ComputeNonRecursive(lines);

            private static int ComputeNonRecursive(IEnumerable<string> lines)
            {
                IEnumerable<int> values = lines.Single().Split(',').Select(int.Parse);

                return SumCost(values, Mean(values));
            }

            private static int SumCost(IEnumerable<int> values, Tuple<int, int> mean)
                => new int[]
                {
                    values.Select(n => Math.Abs(n - mean.Item1)).Select(n => (n * (n - 1) / 2) + n).Sum(),
                    values.Select(n => Math.Abs(n - mean.Item2)).Select(n => (n * (n - 1) / 2) + n).Sum(),
                }.Min();

            private static Tuple<int, int> Mean(IEnumerable<int> values)
                => new((int)double.Floor((double)values.Sum() / values.Count()),
                    (int)double.Ceiling((double)values.Sum() / values.Count()));
        }
    }
}
