using CoreAoC.Entities;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using System.Runtime.CompilerServices;

[assembly: InternalsVisibleTo("TestingProject")]
namespace AdventOfCode.Problems.Y2022
{
    internal partial class P13 : Problem
    {
        internal class P13_1 : Part
        {
            protected override object Compute(IEnumerable<string> lines)
                => ComputeRecursive(lines);

            private static int ComputeRecursive(IEnumerable<string> lines)
            {
                IEnumerable<(Term left, Term right)> packets = new List<(Term left, Term right)>();
                foreach (string[] pair in SplitBlocks(lines))
                    packets = packets.Append((new(pair.First()), new(pair.Last())));

                IEnumerable<int> correctIndexes = packets
                    .Select((packets, idx) => (packets, idx))
                    .Where(tuple => tuple.packets.left.CompareTo(tuple.packets.right) < 0)
                    .Select(tuple => tuple.idx + 1);

                return correctIndexes.Sum();
            }

            private static IEnumerable<string[]> SplitBlocks(IEnumerable<string> lines)
                => string.Join(Environment.NewLine, lines)
                        .Split(new[] { Environment.NewLine + Environment.NewLine }, StringSplitOptions.None)
                        .Select(str => str.Split(new[] { Environment.NewLine }, StringSplitOptions.None));
        }

        internal class P13_2 : Part
        {
            private readonly static Term _FIRST_SEP = new("[[2]]");
            private readonly static Term _LAST_SEP = new("[[6]]");


            protected override object Compute(IEnumerable<string> lines)
                => ComputeRecursive(lines);

            private static int ComputeRecursive(IEnumerable<string> lines)
            {
                Term[] terms = lines
                    .Where(s => !string.IsNullOrEmpty(s))
                    .Select(s => new Term(s))
                    .Append(_FIRST_SEP).Append(_LAST_SEP)
                    .ToArray();
                Array.Sort(terms);

                return (terms.Select((term, idx) => (term, idx)).Single(tuple => tuple.term == _FIRST_SEP).idx + 1)
                     * (terms.Select((term, idx) => (term, idx)).Single(tuple => tuple.term == _LAST_SEP).idx + 1);
            }
        }

        
        private sealed class Term : IComparable<Term>
        {
            public JArray SubTerms { get; }


            public Term(JArray array)
                => SubTerms = array;

            public Term(string line)
                => SubTerms = JsonConvert.DeserializeObject<JArray>(line)!;


            public int CompareTo(Term? other)
            {
                if (other is null)
                    throw new ArgumentException("Null comparision");

                for (int i = 0, limit = new int[] { SubTerms.Count, other.SubTerms.Count }.Max(), tmp; i < limit; i++)
                {
                    if ((tmp = IsAnyOutOfItems(SubTerms, other.SubTerms, i)) != 0)
                        return tmp;

                    if ((tmp = CompareSubTerms(SubTerms[i], other.SubTerms[i])) != 0)
                        return tmp;
                }

                return 0;
            }

            public static bool operator ==(Term left, Term right)
                => left.Equals(right);

            public static bool operator !=(Term left, Term right)
                => !(left == right);

            public static bool operator >(Term left, Term right)
                => left.CompareTo(right) > 0;

            public static bool operator <(Term left, Term right)
                => left.CompareTo(right) < 0;

            public static bool operator >=(Term left, Term right)
                => left.CompareTo(right) >= 0;

            public static bool operator <=(Term left, Term right)
                => left.CompareTo(right) <= 0;


            public override bool Equals(object? obj)
                => obj is Term term && CompareTo(term) == 0;

            public override int GetHashCode()
                => HashCode.Combine(SubTerms);


            private static int IsAnyOutOfItems(JArray left, JArray right, int index)
            {
                if (index >= left.Count && index < right.Count)
                    return -1;
                else if (index >= right.Count && index < left.Count)
                    return 1;
                else
                    return 0;
            }

            private static int CompareSubTerms(JToken leftToken, JToken rightToken)
            {
                Term leftTerm, rightTerm;

                if (leftToken is JValue leftValueSingle && rightToken is JValue rightValueSingle)
                {
                    if ((long)leftValueSingle.Value! != (long)rightValueSingle.Value!)
                        return (long)leftValueSingle.Value! < (long)rightValueSingle.Value! ? -1 : 1;
                    else
                        return 0;
                }
                else if (leftToken is JArray leftArraySingle && rightToken is JArray rightArraySingle)
                {
                    leftTerm = new Term(leftArraySingle);
                    rightTerm = new Term(rightArraySingle);
                }
                else if (leftToken is JArray leftArrayMixed && rightToken is JValue rightValueMixed)
                {
                    leftTerm = new Term(leftArrayMixed);
                    rightTerm = new Term(new JArray(rightValueMixed));
                }
                else if (leftToken is JValue leftValueMixed && rightToken is JArray rightArrayMixed)
                {
                    leftTerm = new Term(new JArray(leftValueMixed));
                    rightTerm = new Term(rightArrayMixed);
                }
                else
                    throw new ArgumentException("Unreacheable code");

                return leftTerm.CompareTo(rightTerm);
            }
        }
    }
}
