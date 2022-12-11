using CoreAoC.Entities;
using System.Runtime.CompilerServices;
using System.Text.RegularExpressions;

[assembly: InternalsVisibleTo("TestingProject")]
namespace AdventOfCode.Problems.Y2022
{
    internal partial class P5 : Problem
    {
        internal partial class P5_1 : Part
        {
            protected override object Compute(IEnumerable<string> lines)
                => ComputeRecursive(lines, false);
        }

        internal partial class P5_2 : Part
        {
            protected override object Compute(IEnumerable<string> lines)
                => ComputeRecursive(lines, true);
        }


        private static string ComputeRecursive(IEnumerable<string> lines, bool reversed)
        {
            IEnumerable<string[]> parts = string.Join(Environment.NewLine, lines)
                    .Split(new[] { Environment.NewLine + Environment.NewLine }, StringSplitOptions.None)
                    .Select(str => str.Split(new[] { Environment.NewLine }, StringSplitOptions.None));

            return ComputeRecursive(parts.Last().AsEnumerable().GetEnumerator(), GetStacks(parts.First()), reversed);
        }

        private static string ComputeRecursive(IEnumerator<string> iter, IList<Stack<char>> stacks, bool reversed)
        {
            if (!iter.MoveNext()) return string.Concat(stacks.Select(s => s.Peek()));

            Match match = Regexp().Match(iter.Current);
            IEnumerable<char> removed = Enumerable.Range(0, int.Parse(match.Groups[1].Value)).Select(_ => stacks[int.Parse(match.Groups[2].Value) - 1].Pop());

            foreach (char c in reversed ? removed.Reverse() : removed)
                stacks[int.Parse(match.Groups[3].Value) - 1].Push(c);

            return ComputeRecursive(iter, stacks, reversed);
        }

        private static IList<Stack<char>> GetStacks(IEnumerable<string> lines)
        {
            IEnumerable<string> rawDefinition = string.Join(Environment.NewLine, lines)
                    .Split(new[] { Environment.NewLine + Environment.NewLine }, StringSplitOptions.None)
                    .Select(str => str.Split(new[] { Environment.NewLine }, StringSplitOptions.None))
                    .First();

            IEnumerable<IEnumerable<Tuple<char, int>>> definitionRows = rawDefinition.Select(GetDefinitionRow);
            IList<Stack<char>> stacks = Enumerable.Range(0, int.Parse(rawDefinition.Last().Trim().Split(' ').Last())).Select(_ => new Stack<char>()).ToList();

            foreach (IEnumerable<Tuple<char, int>> row in definitionRows.SkipLast(1).Reverse())
            {
                foreach (Tuple<char, int> pair in row)
                    stacks[pair.Item2].Push(pair.Item1);
            }

            return stacks;
        }

        private static IEnumerable<Tuple<char, int>> GetDefinitionRow(string line)
                => line.Select((c, i) => (c, i)).Where(t => char.IsUpper(t.c) && t.i % 4 - 1 == 0).Select(t => new Tuple<char, int>(t.c, t.i / 4));


        [GeneratedRegex("move (\\d*) from (\\d*) to (\\d*)")]
        private static partial Regex Regexp();
    }
}
