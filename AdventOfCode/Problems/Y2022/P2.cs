using CoreAoC.Entities;
using System.Runtime.CompilerServices;

[assembly: InternalsVisibleTo("UnitTests")]
namespace AdventOfCode.Problems.Y2022
{
    internal class P2 : Problem
    {
        internal class P2_1 : Part
        {
            protected override string Compute(IEnumerable<string> lines)
                => ComputeRecursive(lines.GetEnumerator(), 0).ToString();

            private static int ComputeRecursive(IEnumerator<string> iter, int result)
            {
                if (!iter.MoveNext()) return result;

                string[] split = iter.Current.Split(' ');
                char opponent = split[0].Single(), player = split[1].Single();

                result += RockPaperScissors(player, opponent);

                return ComputeRecursive(iter, result);
            }

            private static int RockPaperScissors(char player, char opponent)
                #pragma warning disable CS8509
                => player switch
                {
                    'X' => opponent == 'A' ? 3 + 1 : (opponent == 'B' ? 0 : 6) + 1,
                    'Y' => opponent == 'A' ? 6 + 2 : (opponent == 'B' ? 3 : 0) + 2,
                    'Z' => opponent == 'A' ? 0 + 3 : (opponent == 'B' ? 6 : 3) + 3
                };
                #pragma warning restore CS8509
        }

        internal class P2_2 : Part
        {
            protected override string Compute(IEnumerable<string> lines)
                => ComputeRecursive(lines.GetEnumerator(), 0).ToString();

            private static int ComputeRecursive(IEnumerator<string> iter, int result)
            {
                if (!iter.MoveNext()) return result;

                string[] split = iter.Current.Split(' ');
                char opponent = split[0].Single(), expectedResult = split[1].Single();

                result += RockPaperScissors(expectedResult, opponent);

                return ComputeRecursive(iter, result);
            }

            private static int RockPaperScissors(char expectedResult, char opponent)
                #pragma warning disable CS8509
                => expectedResult switch
                {
                    'X' => opponent == 'A' ? 0 + 3 : (opponent == 'B' ? 0 + 1 : 0 + 2),
                    'Y' => opponent == 'A' ? 3 + 1 : (opponent == 'B' ? 3 + 2 : 3 + 3),
                    'Z' => opponent == 'A' ? 6 + 2 : (opponent == 'B' ? 6 + 3 : 6 + 1)
                };
                #pragma warning restore CS8509
        }
    }
}
