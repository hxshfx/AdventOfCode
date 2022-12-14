using CoreAoC.Entities;
using System.Runtime.CompilerServices;

[assembly: InternalsVisibleTo("TestingProject")]
namespace AdventOfCode.Problems.Y2022
{
    internal partial class P12 : Problem
    {
        internal class P12_1 : Part
        {
            protected override object Compute(IEnumerable<string> lines)
                => ComputeNonRecursive(lines, false);
        }

        internal class P12_2 : Part
        {
            protected override object Compute(IEnumerable<string> lines)
                => ComputeNonRecursive(lines, true);
        }


        private static int ComputeNonRecursive(IEnumerable<string> lines, bool inverted)
        {
            (int iStart, int jStart) = GetKeyPosition(lines, 'S');
            (int iEnd, int jEnd) = GetKeyPosition(lines, 'E');

            Graph g = BuildGraph(lines);

            IList<Edge> edges = g.BuildEdges(inverted);
            Func<Vertex, Vertex, bool> endRule = !inverted ? (Vertex v1, Vertex v2) => v1 == v2
                : (Vertex v1, Vertex _) => v1.H == 0;

            BreadthFirstSearcher bfs = new(edges, endRule);

            Vertex start = !inverted ? g.Vertexes.Single(v => v.I == iStart && v.J == jStart)
                : g.Vertexes.Single(v => v.I == iEnd && v.J == jEnd);
            Vertex end = !inverted ? g.Vertexes.Single(v => v.I == iEnd && v.J == jEnd)
                : new('a', -1, -1);

            return bfs.Algorithm(start, end);
        }

        private static Graph BuildGraph(IEnumerable<string> lines)
        {
            Graph graph = new();

            for (int i = 0; i < lines.Count(); i++)
            {
                for (int j = 0; j < lines.ElementAt(i).Length; j++)
                {
                    char c = lines.ElementAt(i).ElementAt(j);
                    if (c == 'S') c = 'a';
                    if (c == 'E') c = 'z';

                    graph.AddVertex(c, i, j);
                }
            }

            return graph;
        }

        private static (int i, int j) GetKeyPosition(IEnumerable<string> lines, char searched)
            => lines.Select((l1, i) => 
                (i, l1.Select((l2, j) => 
                    (l2, j)).Where(t2 => t2.l2 == searched)
                    .SingleOrDefault((default, -1)).j))
                .Single(t => t.j != -1);


        private sealed class Graph
        {
            public IList<Vertex> Vertexes { get; }


            public Graph()
                => Vertexes = new List<Vertex>();


            public void AddVertex(char elevation, int i, int j)
                => Vertexes.Add(new(elevation, i, j));

            public IList<Edge> BuildEdges(bool inverted)
            {
                Func<int, int, bool> edgeRule;

                if (inverted)
                    edgeRule = (int h1, int h2) => h1 - 1 == h2 || h1 == h2 || h2 > h1;
                else
                    edgeRule = (int h1, int h2) => h1 + 1 >= h2;

                return BuildEdges(Vertexes, edgeRule);
            }


            private static IList<Edge> BuildEdges(IList<Vertex> vertexes, Func<int, int, bool> edgeRule)
            {
                IList<Edge> edges = new List<Edge>();

                int maxI = vertexes.Max(v => v.I) + 1,
                    maxJ = vertexes.Max(v => v.J) + 1;

                foreach (int i in Enumerable.Range(0, maxI))
                {
                    foreach (int j in Enumerable.Range(0, maxJ))
                    {
                        Vertex vStart = vertexes.Single(v => v.I == i && v.J == j);
                        foreach ((int i, int j) adjacent in GetAdjacent(vStart, maxI, maxJ))
                        {
                            Vertex vEnd = vertexes.Single(v => v.I == adjacent.i && v.J == adjacent.j);

                            if (edgeRule.Invoke(vStart.H, vEnd.H))
                                edges.Add(new(vStart, vEnd));
                        }
                    }
                }

                return edges;
            }

            private static IEnumerable<(int i, int j)> GetAdjacent(Vertex v, int maxI, int maxJ)
            {
                IEnumerable<(int i, int j)> result = new List<(int i, int j)>();

                if (v.I - 1 >= 0)
                    result = result.Append((v.I - 1, v.J)); // UP
                if (v.I + 1 < maxI)
                    result = result.Append((v.I + 1, v.J)); // DOWN
                if (v.J - 1 >= 0)
                    result = result.Append((v.I, v.J - 1)); // LEFT
                if (v.J + 1 < maxJ)
                    result = result.Append((v.I, v.J + 1)); // RIGHT

                return result;
            }
        }

        private sealed class Edge
        {
            public Vertex Start { get; }
            public Vertex End { get; }


            public Edge(Vertex vStart, Vertex vEnd)
            {
                Start = vStart;
                End = vEnd;
            }

            public override string ToString()
                => $"{Start}  ==>  {End}";

            public override bool Equals(object? obj)
                => obj is Edge edge &&
                        EqualityComparer<Vertex>.Default.Equals(Start, edge.Start) &&
                        EqualityComparer<Vertex>.Default.Equals(End, edge.End);

            public override int GetHashCode()
                => HashCode.Combine(Start, End);
        }

        private sealed class Vertex
        {
            public int H { get; }
            public int I { get; }
            public int J { get; }

            public Vertex? Previous { get; private set; }
            public bool Visited { get; private set; }


            public Vertex(char elevation, int i, int j)
            {
                H = elevation - 'a';

                I = i;
                J = j;
            }


            public void VertexVisited(Vertex? v)
            {
                Previous = v;
                Visited = true;
            }


            public override string ToString()
                => $"({I}, {J}) :: H{H}";

            public override bool Equals(object? obj)
                => obj is Vertex vertex &&
                        I == vertex.I && J == vertex.J;

            public override int GetHashCode()
                => HashCode.Combine(I, J);
        }
    
        private sealed class BreadthFirstSearcher
        {
            private readonly IList<Edge> _edges;
            private readonly Func<Vertex, Vertex, bool> _endRule;


            public BreadthFirstSearcher(IList<Edge> edges, Func<Vertex, Vertex, bool> endRule)
            {
                _edges = edges;
                _endRule = endRule;
            }


            public int Algorithm(Vertex start, Vertex end)
            {
                start.VertexVisited(null);

                Queue<Vertex> queue = new(new Vertex[] { start });
                while (queue.Any())
                {
                    Vertex eval = queue.Dequeue();

                    Vertex? ended;
                    if ((ended = EvaluateVertex(queue, eval, end)) != null)
                    {
                        end = ended;
                        break;
                    }
                }

                return GetPathLength(end);
            }


            private Vertex? EvaluateVertex(Queue<Vertex> queue, Vertex eval, Vertex end)
            {
                foreach (Vertex connected in _edges.Where(e => e.Start == eval).Select(e => e.End))
                {
                    if (!connected.Visited)
                    {
                        connected.VertexVisited(eval);
                        queue.Enqueue(connected);

                        if (_endRule.Invoke(connected, end))
                        {
                            queue.Clear();
                            return connected;
                        }
                    }
                }

                return null;
            }

            private static int GetPathLength(Vertex end)
            {
                int count = 0;

                do
                {
                    if (end.Previous != null)
                    {
                        end = end.Previous;
                        count++;
                    }
                } while (end != null && end.Previous != null);

                return count;
            }
        }
    }
}
