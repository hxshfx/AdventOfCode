using CoreAoC.Entities;
using System.Runtime.CompilerServices;
using System.Text.RegularExpressions;

[assembly: InternalsVisibleTo("TestingProject")]
namespace AdventOfCode.Problems.Y2022
{
    internal partial class P4 : Problem
    {
        internal partial class P4_1 : Part
        {
            protected override object Compute(IEnumerable<string> lines)
                => ComputeRecursive(lines.GetEnumerator(), IsFullyContained, 0).ToString();

            private static bool IsFullyContained(Range r1, Range r2)
                => r1.Start.Value == r2.Start.Value || r1.End.Value == r2.End.Value ||
                   (r1.Start.Value > r2.Start.Value ? r1.End.Value <= r2.End.Value : r1.End.Value >= r2.End.Value);
        }

        internal partial class P4_2 : Part
        {
            protected override object Compute(IEnumerable<string> lines)
                => ComputeRecursive(lines.GetEnumerator(), IsFullyContained, 0).ToString();

            private static bool IsFullyContained(Range r1, Range r2)
                => r2.Start.Value <= r1.Start.Value && r1.Start.Value <= r2.End.Value ||
                   r2.Start.Value <= r1.End.Value && r1.Start.Value <= r2.End.Value;
        }


        private static int ComputeRecursive(IEnumerator<string> iter, Func<Range, Range, bool> isContained, int result)
        {
            if (!iter.MoveNext())
                return result;

            Match match = Regexp().Match(iter.Current);
            Range r1 = new(int.Parse(match.Groups[1].Value), int.Parse(match.Groups[2].Value));
            Range r2 = new(int.Parse(match.Groups[3].Value), int.Parse(match.Groups[4].Value));

            if (isContained.Invoke(r1, r2)) result++;

            return ComputeRecursive(iter, isContained, result);
        }

        [GeneratedRegex("(\\d*)-(\\d*),(\\d*)-(\\d*)")]
        private static partial Regex Regexp();
    }
}
