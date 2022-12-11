using CoreAoC.Entities;
using System.Text.RegularExpressions;
using System.Runtime.CompilerServices;

[assembly: InternalsVisibleTo("TestingProject")]
namespace AdventOfCode.Problems.Y2021
{
    internal partial class P4 : Problem
    {
        internal class P4_1 : Part
        {
            protected override object Compute(IEnumerable<string> lines)
                => ComputeRecursive(lines);


            private static int ComputeRecursive(IEnumerable<string> lines)
            {
                IEnumerable<int> drawn = lines.First().Split(',').Select(l => Convert.ToInt32(l));
                IEnumerable<Bingo> boards = GetAllBoards(lines.Skip(2).GetEnumerator());

                return GetScoreWinningBoard(drawn, boards, -1);
            }

            private static int GetScoreWinningBoard(IEnumerable<int> drawn, IEnumerable<Bingo> boards, int result)
            {
                if (result != -1) return result;

                foreach (Bingo board in boards)
                {
                    board.Update(drawn.First());
                    if (board.IsCompleted)
                        result = drawn.First() * board.GetUnmarkedSum();
                }

                return GetScoreWinningBoard(drawn.Skip(1), boards, result);
            }
        }

        internal class P4_2 : Part
        {
            protected override object Compute(IEnumerable<string> lines)
                => ComputeRecursive(lines);


            private static int ComputeRecursive(IEnumerable<string> lines)
            {
                IEnumerable<int> drawn = lines.First().Split(',').Select(l => Convert.ToInt32(l));
                IEnumerable<Bingo> boards = GetAllBoards(lines.Skip(2).GetEnumerator());

                return GetScoreLastBoard(drawn, boards.ToList());
            }

            private static int GetScoreLastBoard(IEnumerable<int> drawn, IList<Bingo> boards)
            {
                if (boards.Count == 1) return SolveLastBoard(drawn, boards.Single());

                foreach (Bingo board in boards)
                    board.Update(drawn.First());

                return GetScoreLastBoard(drawn.Skip(1), boards.Where(b => !b.IsCompleted).ToList());
            }

            private static int SolveLastBoard(IEnumerable<int> drawn, Bingo board)
            {
                int last = -1;

                while (!board.IsCompleted)
                {
                    last = drawn.First();
                    board.Update(last);
                    drawn = drawn.Skip(1);
                }

                return last * board.GetUnmarkedSum();
            }
        }


        internal partial class Bingo
        {
            public int[][] Board { get; set; }
            public bool IsCompleted { get; set; }

            public Bingo(IEnumerable<string> lines)
            {
                Board = lines.Select(l => Regexp().Split(l.Trim()))
                    .Select(c => c.Select(i => Convert.ToInt32(i)).ToArray()).ToArray();
                IsCompleted = false;
            }

            public void Update(int number)
            {
                var containsIndexes = from i in Enumerable.Range(0, Board.Length)
                                      from j in Enumerable.Range(0, Board[i].Length)
                                      where Board[i][j] == number
                                      select new { i, j };

                if (containsIndexes.Any())
                {
                    (int, int) indexes = (containsIndexes.First().i, containsIndexes.First().j);
                    Board[indexes.Item1][indexes.Item2] = -1;

                    IsCompleted = AnyCompletedRowOrColumn(Board) || AnyCompletedRowOrColumn(Transpose(Board));
                }
            }

            public int GetUnmarkedSum()
                => Board.Select(i => i.Where(j => j != -1).Sum()).Sum();

            private static bool AnyCompletedRowOrColumn(int[][] board)
                => Enumerable.Range(0, board.Length).Select(i => board[i]).Any(i => i.All(j => j == -1));

            private static int[][] Transpose(int[][] board)
                => Enumerable.Range(0, board.Length).Select(i => board.Select(j => j[i]).ToArray()).ToArray();


            [GeneratedRegex("\\s{1,}")]
            private static partial Regex Regexp();
        }

        private static IEnumerable<Bingo> GetAllBoards(IEnumerator<string> iter)
        {
            Bingo? bingo;
            IEnumerable<Bingo> boards = Enumerable.Empty<Bingo>();

            while ((bingo = GetNextBoard(iter)) != null)
                boards = boards.Append(bingo);

            return boards;
        }

        private static Bingo? GetNextBoard(IEnumerator<string> iter)
        {
            IEnumerable<string> boardLines = Enumerable.Empty<string>();

            while (iter.MoveNext() && !iter.Current.Equals(string.Empty))
                boardLines = boardLines.Append(iter.Current);

            return boardLines.Any() ? new Bingo(boardLines) : null;
        }
    }
}
