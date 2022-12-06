using CoreAoC.Entities;
using System.Runtime.CompilerServices;
using System.Text.RegularExpressions;

[assembly: InternalsVisibleTo("UnitTests")]
namespace AdventOfCode.Problems.Y2022
{
    internal class P5 : Problem
    {
        internal class P5_1 : Part
        {
            protected override string Compute(IEnumerable<string> lines)
                => ComputeRecursive(lines);

            private static string ComputeRecursive(IEnumerable<string> lines)
            {
                IEnumerable<string[]> parts = string.Join(Environment.NewLine, lines)
                        .Split(new[] { Environment.NewLine + Environment.NewLine }, StringSplitOptions.None)
                        .Select(str => str.Split(new[] { Environment.NewLine }, StringSplitOptions.None));

                return ComputeRecursive(parts.Last().AsEnumerable().GetEnumerator(), GetStacks(parts.First()));
            }

            private static string ComputeRecursive(IEnumerator<string> iter, IList<Stack<char>> stacks)
            {
                if (!iter.MoveNext()) return string.Concat(stacks.Select(s => s.Peek()));

                Match match = Regex.Match(iter.Current, @"move (\d*) from (\d*) to (\d*)");
                IEnumerable<char> removed = Enumerable.Range(0, int.Parse(match.Groups[1].Value)).Select(_ => stacks[int.Parse(match.Groups[2].Value) - 1].Pop());

                foreach (char c in removed)
                    stacks[int.Parse(match.Groups[3].Value) - 1].Push(c);

                return ComputeRecursive(iter, stacks);
            }

            private static IList<Stack<char>> GetStacks(IEnumerable<string> lines)
            {
                IEnumerable<string> rawDefinition = string.Join(Environment.NewLine, lines)
                        .Split(new[] { Environment.NewLine + Environment.NewLine }, StringSplitOptions.None)
                        .Select(str => str.Split(new[] { Environment.NewLine }, StringSplitOptions.None))
                        .First();

                IEnumerable<IEnumerable<Tuple<char, int>>> definitionRows = rawDefinition.Select(s => GetDefinitionRow(s));
                IList<Stack<char>> stacks = Enumerable.Range(0, int.Parse(rawDefinition.Last().Trim().Split(' ').Last())).Select(_ => new Stack<char>()).ToList();

                foreach (IEnumerable<Tuple<char, int>> row in definitionRows.SkipLast(1).Reverse())
                {
                    foreach (Tuple<char, int> pair in row)
                        stacks[pair.Item2].Push(pair.Item1);
                }

                return stacks;
            }

            private static IEnumerable<Tuple<char, int>> GetDefinitionRow(string line)
                => line.Select((c, i) => (c, i)).Where(t => char.IsUpper(t.c) && (t.i % 4) - 1 == 0).Select(t => new Tuple<char, int>(t.c, t.i / 4));
        }

        internal class P5_2 : Part
        {
            protected override string Compute(IEnumerable<string> lines)
                => ComputeRecursive(lines);

            private static string ComputeRecursive(IEnumerable<string> lines)
            {
                IEnumerable<string[]> parts = string.Join(Environment.NewLine, lines)
                        .Split(new[] { Environment.NewLine + Environment.NewLine }, StringSplitOptions.None)
                        .Select(str => str.Split(new[] { Environment.NewLine }, StringSplitOptions.None));

                return ComputeRecursive(parts.Last().AsEnumerable().GetEnumerator(), GetStacks(parts.First()));
            }

            private static string ComputeRecursive(IEnumerator<string> iter, IList<Stack<char>> stacks)
            {
                if (!iter.MoveNext()) return string.Concat(stacks.Select(s => s.Peek()));

                Match match = Regex.Match(iter.Current, @"move (\d*) from (\d*) to (\d*)");
                IEnumerable<char> removed = Enumerable.Range(0, int.Parse(match.Groups[1].Value)).Select(_ => stacks[int.Parse(match.Groups[2].Value) - 1].Pop());

                foreach (char c in removed.Reverse())
                    stacks[int.Parse(match.Groups[3].Value) - 1].Push(c);

                return ComputeRecursive(iter, stacks);
            }

            private static IList<Stack<char>> GetStacks(IEnumerable<string> lines)
            {
                IEnumerable<string> rawDefinition = string.Join(Environment.NewLine, lines)
                        .Split(new[] { Environment.NewLine + Environment.NewLine }, StringSplitOptions.None)
                        .Select(str => str.Split(new[] { Environment.NewLine }, StringSplitOptions.None))
                        .First();

                IEnumerable<IEnumerable<Tuple<char, int>>> definitionRows = rawDefinition.Select(s => GetDefinitionRow(s));
                IList<Stack<char>> stacks = Enumerable.Range(0, int.Parse(rawDefinition.Last().Trim().Split(' ').Last())).Select(_ => new Stack<char>()).ToList();

                foreach (IEnumerable<Tuple<char, int>> row in definitionRows.SkipLast(1).Reverse())
                {
                    foreach (Tuple<char, int> pair in row)
                        stacks[pair.Item2].Push(pair.Item1);
                }

                return stacks;
            }

            private static IEnumerable<Tuple<char, int>> GetDefinitionRow(string line)
                => line.Select((c, i) => (c, i)).Where(t => char.IsUpper(t.c) && (t.i % 4) - 1 == 0).Select(t => new Tuple<char, int>(t.c, t.i / 4));
        }
    }
}
