using CoreAoC.Entities;
using System.Runtime.CompilerServices;

[assembly: InternalsVisibleTo("TestingProject")]
namespace AdventOfCode.Problems.Y2020
{
    internal class P6 : Problem
    {
        internal class P6_1 : Part
        {
            protected override object Compute(IEnumerable<string> lines)
                => ComputeRecursive(lines.GetEnumerator(), 0, null);


            private static int ComputeRecursive(IEnumerator<string> iter, int result, string[]? currentGroup)
            {
                if (currentGroup == null) currentGroup = GetNextGroup(iter);

                if (currentGroup.Length == 0) return result;

                result += string.Join("", currentGroup).Distinct().Count();

                return ComputeRecursive(iter, result, GetNextGroup(iter));
            }

            private static string[] GetNextGroup(IEnumerator<string> iter)
            {
                IList<string> group = new List<string>();

                while (iter.MoveNext() && !iter.Current.Equals(string.Empty))
                    group.Add(iter.Current);

                return group.ToArray();
            }
        }

        internal class P6_2 : Part
        {
            private readonly static IEnumerable<char> ALPHABET = Enumerable.Range('a', 26).Select(x => (char)x).ToArray();


            protected override object Compute(IEnumerable<string> lines)
                => ComputeRecursive(lines.GetEnumerator(), 0, null);


            private static int ComputeRecursive(IEnumerator<string> iter, int result, string[]? currentGroup)
            {
                if (currentGroup == null) currentGroup = GetNextGroup(iter);

                if (currentGroup.Length == 0) return result;

                result += GetEveryOccurrence(currentGroup);

                return ComputeRecursive(iter, result, GetNextGroup(iter));
            }

            private static string[] GetNextGroup(IEnumerator<string> iter)
            {
                IList<string> group = new List<string>();

                while (iter.MoveNext() && !iter.Current.Equals(string.Empty))
                    group.Add(iter.Current);

                return group.ToArray();
            }

            private static int GetEveryOccurrence(string[] group)
                => ALPHABET.Select(c => c).Count(c => IsOnEveryLine(group, c));

            private static bool IsOnEveryLine(string[] group, char searched)
                => group.All(s => s.Contains(searched));
        }
    }
}
