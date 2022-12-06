using CoreAoC.Entities;
using System.Runtime.CompilerServices;

[assembly: InternalsVisibleTo("UnitTests")]
namespace AdventOfCode.Problems.Y2021
{
    internal class P5 : Problem
    {
        internal class P5_1 : Part
        {
            protected override string Compute(IEnumerable<string> lines)
                => ComputeNonRecursive(lines.GetEnumerator()).ToString();


            private static int ComputeNonRecursive(IEnumerator<string> iter)
            {
                (IEnumerable<Line> lines, (int, int) size) = GetLinesAndDiagramSize(iter, true);
                Diagram d = new(size.Item1, size.Item2);

                foreach (Line line in lines)
                    d.UpdateNonDiagonal(line);

                return d.GetOverlapSum();
            }
        }

        internal class P5_2 : Part
        {
            protected override string Compute(IEnumerable<string> lines)
                => ComputeNonRecursive(lines.GetEnumerator()).ToString();


            private static int ComputeNonRecursive(IEnumerator<string> iter)
            {
                (IEnumerable<Line> allLines, (int, int) size) = GetLinesAndDiagramSize(iter);
                Diagram d = new(size.Item1, size.Item2);

                IEnumerable<Line> verticalLines = allLines.Where(l => l.IsVertical());
                IEnumerable<Line> diagonalLines = allLines.Except(verticalLines);

                foreach (Line line in verticalLines)
                    d.UpdateNonDiagonal(line);

                foreach (Line line in diagonalLines)
                    d.UpdateDiagonal(line);

                return d.GetOverlapSum();
            }
        }


        internal class Line
        {
            public (int, int) Start { get; set; }

            public (int, int) End { get; set; }


            public Line(string line)
            {
                string[] split = line.Split(" -> ");

                int[] start = split[0].Split(',').Select(i => Convert.ToInt32(i)).ToArray();
                int[] end = split[1].Split(',').Select(i => Convert.ToInt32(i)).ToArray();

                if (start[0] == end[0] && start[1] > end[1] || start[1] == end[1] && start[0] > end[0])
                {
                    Start = (end[0], end[1]);
                    End = (start[0], start[1]);
                }
                else
                {
                    Start = (start[0], start[1]);
                    End = (end[0], end[1]);
                }
            }

            public bool IsVertical()
                => Start.Item1 == End.Item1 || Start.Item2 == End.Item2;
        }

        internal class Diagram
        {
            int[,] Board { get; set; }

            public Diagram(int tall, int wide)
                => Board = new int[tall + 1, wide + 1];

            public void UpdateNonDiagonal(Line line)
            {
                bool fixedX = IsFixedX(line);
                IEnumerable<(int, int)> linePath = GetNonDiagonalLinePath(line, fixedX, GetNonDiagonalLength(line, fixedX));

                foreach ((int, int) position in linePath)
                    Board[position.Item2, position.Item1]++;
            }

            public void UpdateDiagonal(Line line)
            {
                IEnumerable<(int, int)> res = GetDiagonalLinePath(line, GetDiagonalLength(line));

                foreach ((int, int) position in res)
                    Board[position.Item2, position.Item1]++;
            }

            public int GetOverlapSum()
            {
                int res = 0;
                for (int i = 0; i < Board.GetLength(0); i++)
                {
                    for (int j = 0; j < Board.GetLength(1); j++)
                        res = Board[i, j] > 1 ? res + 1 : res;
                }

                return res;
            }

            private static bool IsFixedX(Line line)
                => line.Start.Item1 == line.End.Item1;

            private static int GetNonDiagonalLength(Line line, bool fixedX)
                => fixedX ? line.End.Item2 - line.Start.Item2 + 1 : line.End.Item1 - line.Start.Item1 + 1;

            private static int GetDiagonalLength(Line line)
                => Math.Abs(line.End.Item1 - line.Start.Item1) + 1;

            private static IEnumerable<(int, int)> GetNonDiagonalLinePath(Line line, bool fixedX, int length)
                => fixedX ? GetNonDiagonalLinePathX(line, length) : GetNonDiagonalLinePathY(line, length);

            private static IEnumerable<(int, int)> GetNonDiagonalLinePathX(Line line, int length)
                => Enumerable.Zip(Enumerable.Repeat(line.Start.Item1, length), Enumerable.Range(line.Start.Item2, length + 1));

            private static IEnumerable<(int, int)> GetNonDiagonalLinePathY(Line line, int length)
                => Enumerable.Zip(Enumerable.Range(line.Start.Item1, length + 1), Enumerable.Repeat(line.Start.Item2, length));

            private static IEnumerable<(int, int)> GetDiagonalLinePath(Line line, int length)
                => Enumerable.Zip(GetDiagonalLinePathX(line, length), GetDiagonalLinePathY(line, length));

            private static IEnumerable<int> GetDiagonalLinePathX(Line line, int length)
                => line.Start.Item1 > line.End.Item1 ?
                    Enumerable.Range(line.End.Item1, length).Reverse() :
                    Enumerable.Range(line.Start.Item1, length);

            private static IEnumerable<int> GetDiagonalLinePathY(Line line, int length)
                => line.Start.Item2 > line.End.Item2 ?
                    Enumerable.Range(line.End.Item2, length).Reverse() :
                    Enumerable.Range(line.Start.Item2, length);
        }

        private static (IEnumerable<Line>, (int, int)) GetLinesAndDiagramSize(IEnumerator<string> iter, bool vertical = false)
        {
            IEnumerable<Line> lines = Enumerable.Empty<Line>();
            int sizeX = 0, sizeY = 0;

            while (iter.MoveNext())
            {
                lines = lines.Append(new Line(iter.Current));
                sizeX = lines.Last().End.Item1 > sizeX ? lines.Last().End.Item1 : sizeX;
                sizeY = lines.Last().End.Item2 > sizeY ? lines.Last().End.Item2 : sizeY;
            }

            return (lines.Where(l => !vertical || l.IsVertical()), (sizeY, sizeX));
        }
    }
}
