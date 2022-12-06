using CoreAoC.Entities;
using System.Runtime.CompilerServices;

[assembly: InternalsVisibleTo("UnitTests")]
namespace AdventOfCode.Problems.Y2022
{
    internal class P3 : Problem
    {
        internal class P3_1 : Part
        {
            protected override string Compute(IEnumerable<string> lines)
                => ComputeRecursive(lines.GetEnumerator(), 0).ToString();

            private static int ComputeRecursive(IEnumerator<string> iter, int result)
            {
                if (!iter.MoveNext())
                    return result;

                char[] bag1 = iter.Current.Take(iter.Current.Length / 2).Select(c => c).ToArray();
                char[] bag2 = iter.Current.Skip(iter.Current.Length / 2).Select(c => c).ToArray();
                char common = bag1.Intersect(bag2).Single();

                result += GetPriority(common);

                return ComputeRecursive(iter, result);
            }

            private static int GetPriority(char c)
                => char.IsLower(c) ? c - ('a' - 1) : c - ('A' - 1) + 26;
        }

        internal class P3_2 : Part
        {
            protected override string Compute(IEnumerable<string> lines)
                => ComputeRecursive(lines.Select((l, i) => (l, i))
                        .GroupBy(l => l.i / 3)
                        .Select(l => l.Select(l => l.l).ToArray())
                    .GetEnumerator(), 0).ToString();

            private static int ComputeRecursive(IEnumerator<string[]> iter, int result)
            {
                if (!iter.MoveNext())
                    return result;

                char[] bag1 = iter.Current.Skip(0).First().Select(c => c).ToArray();
                char[] bag2 = iter.Current.Skip(1).First().Select(c => c).ToArray();
                char[] bag3 = iter.Current.Skip(2).First().Select(c => c).ToArray();
                char common = bag1.Intersect(bag2).Intersect(bag3).Single();

                result += GetPriority(common);

                return ComputeRecursive(iter, result);
            }

            private static int GetPriority(char c)
                => char.IsLower(c) ? c - ('a' - 1) : c - ('A' - 1) + 26;
        }
    }
}
