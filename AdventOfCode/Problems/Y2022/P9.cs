using CoreAoC.Entities;
using System.Runtime.CompilerServices;

[assembly: InternalsVisibleTo("TestingProject")]
namespace AdventOfCode.Problems.Y2022
{
    internal class P9 : Problem
    {
        internal class P9_1 : Part
        {
            private const int _NODE_LIMIT = 2;

            protected override object Compute(IEnumerable<string> lines)
                => ComputeNonRecursive(lines, _NODE_LIMIT);
        }

        internal class P9_2 : Part
        {
            private const int _NODE_LIMIT = 10;

            protected override object Compute(IEnumerable<string> lines)
                => ComputeNonRecursive(lines, _NODE_LIMIT);
        }


        private static int ComputeNonRecursive(IEnumerable<string> lines, int nodeLimit)
        {
            Chain chain = new(nodeLimit);

            IEnumerable<(EDirection dir, int count)> split = lines
                .Select(l => l.Split(' ')
                .Select(s => s.ToCharArray()))
                .Select(s =>
                    (GetDir(s.First().Single()),
                    int.Parse(s.Last())));

            foreach ((EDirection dir, int count) in split)
            {
                for (int _ = 0; _ < count; _++)
                    chain.MoveDir(new EDirection[] { dir }, 0);
            }

            return chain.GetVisited();
        }


        private enum EDirection
        { UP, DOWN, LEFT, RIGHT }

        private static EDirection GetDir(char c)
            => c switch
            {
                'U' => EDirection.UP,
                'D' => EDirection.DOWN,
                'L' => EDirection.LEFT,
                'R' => EDirection.RIGHT,
                _ => throw new ArgumentException("Unreachable code")
            };


        private sealed class Chain
        {
            private readonly IList<Position> _chainCoordinates;
            private readonly HashSet<Position> _visited;

            private readonly int _nodeLimit;


            public Chain(int nodeLimit)
            {
                _chainCoordinates = new List<Position>() { new(0, 0) };
                _visited = new HashSet<Position>() { new(0, 0) };

                _nodeLimit = nodeLimit;
            }


            public void MoveDir(IEnumerable<EDirection> dirs, int headIdx)
            {
                Position headStart = _chainCoordinates[headIdx];
                Position headEnd = GetStep(dirs, headStart);

                _chainCoordinates[headIdx] = headEnd;

                if (headIdx == _nodeLimit - 1)
                    _visited.Add(headEnd);

                if (headIdx + 1 == _chainCoordinates.Count)
                {
                    if (_chainCoordinates.Count < _nodeLimit)
                        _chainCoordinates.Add(new(headStart.I, headStart.J));
                }
                else
                {
                    Position tail = _chainCoordinates[headIdx + 1];

                    if (!IsAdjacent(headEnd, tail))
                    {
                        dirs = Follow(headStart, headEnd, tail);
                        MoveDir(dirs, headIdx + 1);
                    }
                }
            }

            public int GetVisited()
                => _visited.Count;


            private static Position GetStep(IEnumerable<EDirection> dirs, Position p)
            {
                foreach (EDirection dir in dirs)
                    p = GetNextStep(dir, p);

                return p;
            }

            private static Position GetNextStep(EDirection dir, Position p)
                => dir switch
                {
                    EDirection.UP => new(p.I - 1, p.J),
                    EDirection.DOWN => new(p.I + 1, p.J),
                    EDirection.LEFT => new(p.I, p.J - 1),
                    EDirection.RIGHT => new(p.I, p.J + 1),
                    _ => throw new ArgumentException("Unreachable code")
                };

            private static bool IsAdjacent(Position head, Position tail)
                => IsInOrtho(head, tail, 1) || IsInDiag(head, tail) || head == tail;

            private static bool IsInOrtho(Position head, Position tail, int distance)
                => (head.I - distance == tail.I && head.J == tail.J) || // UP
                   (head.J - distance == tail.J && head.I == tail.I) || // LEFT
                   (head.I + distance == tail.I && head.J == tail.J) || // DOWN
                   (head.J + distance == tail.J && head.I == tail.I);   // RIGHT

            private static bool IsInDiag(Position head, Position tail)
                => (tail.I - 1 == head.I && tail.J - 1 == head.J) || // UPLEFT
                   (tail.I - 1 == head.I && tail.J + 1 == head.J) || // UPRIGHT
                   (tail.I + 1 == head.I && tail.J - 1 == head.J) || // DOWNLEFT
                   (tail.I + 1 == head.I && tail.J + 1 == head.J);   // DOWNRIGHT

            private static EDirection[] Follow(Position headStart, Position headEnd, Position tail)
            {
                int diagonalDistance;
                if (IsInDiag(tail, headStart))
                {
                    diagonalDistance = 2;

                    if (IsInOrtho(tail, headEnd, diagonalDistance))
                        return FollowOrthoDirection(tail, headEnd, diagonalDistance);
                    else
                        return FollowDiagonalDirection(tail, headStart);
                }
                else
                {
                    diagonalDistance = 1;

                    if (IsInOrtho(headEnd, headStart, diagonalDistance))
                        return FollowOrthoDirection(headStart, headEnd, diagonalDistance);
                    else
                        return FollowDiagonalDirection(headStart, headEnd);
                }
            }

            private static EDirection[] FollowOrthoDirection(Position head, Position tail, int distance)
            {
                if (head.I - distance == tail.I && head.J == tail.J) return new EDirection[] { EDirection.UP };
                else if (head.I + distance == tail.I && head.J == tail.J) return new EDirection[] { EDirection.DOWN };
                else if (head.I == tail.I && head.J - distance == tail.J) return new EDirection[] { EDirection.LEFT };
                else if (head.I == tail.I && head.J + distance == tail.J) return new EDirection[] { EDirection.RIGHT };
                else throw new ArgumentException("Unreachable code");
            }

            private static EDirection[] FollowDiagonalDirection(Position head, Position tail)
            {
                if (head.I - 1 == tail.I && head.J - 1 == tail.J) return new EDirection[] { EDirection.UP, EDirection.LEFT };
                else if (head.I - 1 == tail.I && head.J + 1 == tail.J) return new EDirection[] { EDirection.UP, EDirection.RIGHT };
                else if (head.I + 1 == tail.I && head.J - 1 == tail.J) return new EDirection[] { EDirection.DOWN, EDirection.LEFT };
                else if (head.I + 1 == tail.I && head.J + 1 == tail.J) return new EDirection[] { EDirection.DOWN, EDirection.RIGHT };
                else throw new ArgumentException("Unreachable code");
            }


            private readonly struct Position
            {
                public int I { get; }
                public int J { get; }


                public Position(int i, int j)
                {
                    I = i;
                    J = j;
                }


                public override bool Equals(object? obj)
                    => obj is Position position && I == position.I && J == position.J;

                public override int GetHashCode()
                    => HashCode.Combine(I, J);


                public static bool operator ==(Position c1, Position c2)
                    => c1.Equals(c2);

                public static bool operator !=(Position c1, Position c2)
                    => !c1.Equals(c2);
            }
        }
    }
}
