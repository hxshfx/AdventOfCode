using CoreAoC.Entities;
using System.Runtime.CompilerServices;

[assembly: InternalsVisibleTo("TestingProject")]
namespace AdventOfCode.Problems.Y2022
{
    internal class P1 : Problem
    {
        internal class P1_1 : Part
        {
            protected override object Compute(IEnumerable<string> lines)
                => ComputeRecursive(SplitBlocks(lines), 0);

            private static int ComputeRecursive(IEnumerator<string[]> iter, int result)
            {
                if (!iter.MoveNext()) return result;

                if ((iter.Current.Sum(int.Parse) is int tmp) && tmp > result) result = tmp;

                return ComputeRecursive(iter, result);
            }
        }

        internal class P1_2 : Part
        {
            protected override object Compute(IEnumerable<string> lines)
                => ComputeRecursive(SplitBlocks(lines), new List<int>());

            private static int ComputeRecursive(IEnumerator<string[]> iter, IEnumerable<int> result)
            {
                if (!iter.MoveNext()) return result.OrderByDescending(r => r).Take(3).Sum();

                result = result.Append(iter.Current.Sum(int.Parse));

                return ComputeRecursive(iter, result);
            }
        }


        private static IEnumerator<string[]> SplitBlocks(IEnumerable<string> lines)
            => string.Join(Environment.NewLine, lines)
                        .Split(new[] { Environment.NewLine + Environment.NewLine }, StringSplitOptions.None)
                        .Select(str => str.Split(new[] { Environment.NewLine }, StringSplitOptions.None))
                    .GetEnumerator();
    }
}
