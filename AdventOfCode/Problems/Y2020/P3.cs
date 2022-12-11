using CoreAoC.Entities;
using System.Runtime.CompilerServices;

[assembly: InternalsVisibleTo("TestingProject")]
namespace AdventOfCode.Problems.Y2020
{
    internal class P3 : Problem
    {
        private const char TREE = '#';

        internal class P3_1 : Part
        {
            private const int ROW_MOVEMENT = 1;
            private const int COL_MOVEMENT = 3;


            protected override object Compute(IEnumerable<string> lines)
                => ComputeRecursive(lines);


            private static int ComputeRecursive(IEnumerable<string> lines)
            {
                char[][] board = BuildBoard(lines.ToArray());
                return TreesFound(board, 0, 0);
            }

            private static int TreesFound(char[][] board, int step, int result)
            {
                if ((step * ROW_MOVEMENT) >= board.GetLength(0)) return result;

                if (IsTree(board, GetPosition(board.Length, board[0].Length, step))) result++;

                return TreesFound(board, ++step, result);
            }

            private static char[][] BuildBoard(string[] lines)
                => lines.Select(l => l.ToArray()).ToArray();

            private static bool IsTree(char[][] board, (int, int) position)
                => board[position.Item1][position.Item2] == TREE;

            private static (int, int) GetPosition(int rows, int columns, int step)
                => (step * ROW_MOVEMENT % rows, step * COL_MOVEMENT % columns);
        }

        internal class P3_2 : Part
        {
            private static readonly int[] ROW_MOVEMENTS = new int[] { 1, 1, 1, 1, 2 };
            private static readonly int[] COL_MOVEMENTS = new int[] { 1, 3, 5, 7, 1 };

            protected override object Compute(IEnumerable<string> lines)
                => ComputeRecursive(lines);


            private static long ComputeRecursive(IEnumerable<string> lines)
            {
                char[][] board = BuildBoard(lines.ToArray());
                long result = 1;

                foreach ((int, int) movement in ROW_MOVEMENTS.Zip(COL_MOVEMENTS))
                    result *= TreesFound(board, 0, movement, 0);

                return result;
            }

            private static long TreesFound(char[][] board, int step, (int, int) movement, long result)
            {
                if ((step * movement.Item1) >= board.GetLength(0)) return result;

                if (IsTree(board, GetPosition(board.Length, board[0].Length, step, movement))) result++;

                return TreesFound(board, ++step, movement, result);
            }

            private static char[][] BuildBoard(string[] lines)
                => lines.Select(l => l.ToArray()).ToArray();

            private static bool IsTree(char[][] board, (int, int) position)
                => board[position.Item1][position.Item2] == TREE;

            private static (int, int) GetPosition(int rows, int columns, int step, (int, int) movement)
                => (step * movement.Item1 % rows, step * movement.Item2 % columns);
        }
    }
}
