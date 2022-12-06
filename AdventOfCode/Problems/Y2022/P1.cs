using CoreAoC.Entities;
using System.Runtime.CompilerServices;

[assembly: InternalsVisibleTo("UnitTests")]
namespace AdventOfCode.Problems.Y2022
{
    internal class P1 : Problem
    {
        internal class P1_1 : Part
        {
            protected override string Compute(IEnumerable<string> lines)
                => ComputeRecursive(string.Join(Environment.NewLine, lines)
                        .Split(new[] { Environment.NewLine + Environment.NewLine }, StringSplitOptions.None)
                        .Select(str => str.Split(new[] { Environment.NewLine }, StringSplitOptions.None))
                    .GetEnumerator(), 0).ToString();

            private static int ComputeRecursive(IEnumerator<string[]> iter, int result)
            {
                if (!iter.MoveNext()) return result;

                if (iter.Current.Sum(l => int.Parse(l)) > result) result = iter.Current.Sum(l => int.Parse(l));

                return ComputeRecursive(iter, result);
            }
        }

        internal class P1_2 : Part
        {
            protected override string Compute(IEnumerable<string> lines)
                => ComputeRecursive(string.Join(Environment.NewLine, lines)
                    .Split(new[] { Environment.NewLine + Environment.NewLine }, StringSplitOptions.None)
                    .Select(str => str.Split(new[] { Environment.NewLine }, StringSplitOptions.None)).GetEnumerator(), new List<int>()).ToString();

            private static int ComputeRecursive(IEnumerator<string[]> iter, IEnumerable<int> result)
            {
                if (!iter.MoveNext()) return result.OrderByDescending(r => r).Take(3).Sum();

                result = result.Append(iter.Current.Sum(l => int.Parse(l)));

                return ComputeRecursive(iter, result);
            }
        }
    }
}
