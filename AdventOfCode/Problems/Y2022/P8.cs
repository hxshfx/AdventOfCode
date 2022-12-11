using CoreAoC.Entities;
using System.Runtime.CompilerServices;

[assembly: InternalsVisibleTo("TestingProject")]
namespace AdventOfCode.Problems.Y2022
{
    internal class P8 : Problem
    {
        internal class P8_1 : Part
        {
            protected override object Compute(IEnumerable<string> lines)
                => ComputeNonRecursive(lines, IsVisible, res => res.Count(b => b)).ToString();


            private static bool IsVisible(int[][] matrix, int i, int j)
                => IsVisibleOuter(matrix, i, j) || IsVisibleInner(matrix, i, j);

            private static bool IsVisibleOuter(int[][] matrix, int i, int j)
                => i == 0 || j == 0 || i == matrix.Length - 1 || j == matrix[i].Length - 1;

            private static bool IsVisibleInner(int[][] matrix, int i, int j)
                => Enum.GetValues<EDirection>().Any(dir => IsVisibleFromDir(dir, matrix, i, j));

            private static bool IsVisibleFromDir(EDirection dir, int[][] grid, int i, int j)
            {
                bool result = true;
                int height = grid[i][j];

                for (int k = GetStart(dir, i, j); GetCondition(dir, grid, k) && result; k = GetStep(dir, k))
                    result &= GetIfVisible(dir, grid, height, i, j, k);

                return result;
            }
        }

        internal class P8_2 : Part
        {
            protected override object Compute(IEnumerable<string> lines)
                => ComputeNonRecursive(lines, CalculateScore, res => res.Max()).ToString();


            private static int CalculateScore(int[][] matrix, int i, int j)
            {
                int result = 1;

                foreach (EDirection dir in Enum.GetValues<EDirection>())
                    result *= ScoreFromDir(dir, matrix, i, j);

                return result;
            }

            private static int ScoreFromDir(EDirection dir, int[][] matrix, int i, int j)
            {
                int height = matrix[i][j], result = 0;

                for (int k = GetStart(dir, i, j); GetCondition(dir, matrix, k); k = GetStep(dir, k))
                {
                    result++;

                    if (!GetIfVisible(dir, matrix, height, i, j, k))
                        break;
                }

                return result;
            }
        }


        private static int ComputeNonRecursive<TAppend>(IEnumerable<string> lines, Func<int[][], int, int, TAppend> appender, Func<IEnumerable<TAppend>, int> solver)
        {
            IEnumerable<TAppend> result = new List<TAppend>();

            int[][] grid = lines.Select(l => l.Select(c => (int)char.GetNumericValue(c)).ToArray()).ToArray();
            for (int i = 0; i < grid.Length; i++)
            {
                for (int j = 0; j < grid[i].Length; j++)
                    result = result.Append(appender.Invoke(grid, i, j));
            }

            return solver.Invoke(result);
        }


        internal enum EDirection
        { TOP, BOTTOM, LEFT, RIGHT }


        #pragma warning disable CS8524
        public static int GetStart(EDirection dir, int i, int j)
            => dir switch
            {
                EDirection.TOP => i - 1,
                EDirection.BOTTOM => i + 1,
                EDirection.LEFT => j - 1,
                EDirection.RIGHT => j + 1,
            };

        public static bool GetCondition(EDirection dir, int[][] grid, int k)
            => dir switch
            {
                EDirection.TOP or EDirection.LEFT => k >= 0,
                EDirection.BOTTOM => k < grid.Length,
                EDirection.RIGHT => k < grid[0].Length
            };

        public static int GetStep(EDirection dir, int k)
            => dir switch
            {
                EDirection.TOP or EDirection.LEFT => k - 1,
                EDirection.BOTTOM or EDirection.RIGHT => k + 1
            };

        public static bool GetIfVisible(EDirection dir, int[][] grid, int height, int i, int j, int k)
            => dir switch
            {
                EDirection.TOP or EDirection.BOTTOM => grid[k][j] < height,
                EDirection.LEFT or EDirection.RIGHT => grid[i][k] < height
            };
        #pragma warning restore CS8524
    }
}
