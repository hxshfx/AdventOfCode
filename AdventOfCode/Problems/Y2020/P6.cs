using AdventOfCode.Utils;

namespace AdventOfCode.Problems.Y2020
{
    internal class P6 : Problem
    {
        public override (Part, Part) Parts { get; set; }


        public P6(string inputPath) : base(inputPath)
            => Parts = (new P6_1(), new P6_2());


        internal class P6_1 : Part
        {
            public override Result Compute(IEnumerable<string> lines)
                => new(ComputeRecursive(lines.GetEnumerator(), 0, null).ToString(), Sw.ElapsedMilliseconds);


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


            public override Result Compute(IEnumerable<string> lines)
                => new(ComputeRecursive(lines.GetEnumerator(), 0, null).ToString(), Sw.ElapsedMilliseconds);


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
