using CoreAoC.Entities;
using System.Runtime.CompilerServices;
using System.Text.RegularExpressions;

[assembly: InternalsVisibleTo("UnitTests")]
namespace AdventOfCode.Problems.Y2022
{
    internal class P4 : Problem
    {
        internal class P4_1 : Part
        {
            protected override string Compute(IEnumerable<string> lines)
                => ComputeRecursive(lines.GetEnumerator(), 0).ToString();

            private static int ComputeRecursive(IEnumerator<string> iter, int result)
            {
                if (!iter.MoveNext())
                    return result;

                Match match = Regex.Match(iter.Current, @"(\d*)-(\d*),(\d*)-(\d*)");
                Range r1 = new(int.Parse(match.Groups[1].Value), int.Parse(match.Groups[2].Value));
                Range r2 = new(int.Parse(match.Groups[3].Value), int.Parse(match.Groups[4].Value));

                if (IsFullyContained(r1, r2)) result++;

                return ComputeRecursive(iter, result);
            }

            private static bool IsFullyContained(Range r1, Range r2)
                => (r1.Start.Value == r2.Start.Value) || (r1.End.Value == r2.End.Value) ||
                   (r1.Start.Value > r2.Start.Value ? r1.End.Value <= r2.End.Value : r1.End.Value >= r2.End.Value);
        }

        internal class P4_2 : Part
        {
            protected override string Compute(IEnumerable<string> lines)
                => ComputeRecursive(lines.GetEnumerator(), 0).ToString();

            private static int ComputeRecursive(IEnumerator<string> iter, int result)
            {
                if (!iter.MoveNext())
                    return result;

                Match match = Regex.Match(iter.Current, @"(\d*)-(\d*),(\d*)-(\d*)");
                Range r1 = new(int.Parse(match.Groups[1].Value), int.Parse(match.Groups[2].Value));
                Range r2 = new(int.Parse(match.Groups[3].Value), int.Parse(match.Groups[4].Value));

                if (IsFullyContained(r1, r2)) result++;

                return ComputeRecursive(iter, result);
            }

            private static bool IsFullyContained(Range r1, Range r2)
                => (r2.Start.Value <= r1.Start.Value && r1.Start.Value <= r2.End.Value) ||
                   (r2.Start.Value <= r1.End.Value && r1.Start.Value <= r2.End.Value);
        }
    }
}
