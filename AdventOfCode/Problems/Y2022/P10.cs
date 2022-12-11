using CoreAoC.Entities;
using System.Runtime.CompilerServices;
using System.Text;
using System.Text.RegularExpressions;

[assembly: InternalsVisibleTo("TestingProject")]
namespace AdventOfCode.Problems.Y2022
{
    internal partial class P10 : Problem
    {
        internal class P10_1 : Part
        {
            protected override object Compute(IEnumerable<string> lines)
                => ComputeNonRecursive(lines);

            private static int ComputeNonRecursive(IEnumerable<string> lines)
            {
                Processor proc = new();
                proc.Run(lines);

                return proc.SignalSum();
            }
        }

        internal class P10_2 : Part
        {
            protected override object Compute(IEnumerable<string> lines)
                => ComputeNonRecursive(lines);

            private static string ComputeNonRecursive(IEnumerable<string> lines)
            {
                Processor proc = new();
                proc.Run(lines);

                return proc.ScreenLetters();
            }
        }


        private sealed partial class Processor
        {
            private int _cycle;
            private int _registerValue;

            private readonly IList<int> _milestones 
                = Enumerable.Range(20, 220).Where(i => (i - 20) % 40 == 0).ToList();
            private readonly int[] _strengths 
                = new int[6];

            private readonly char[][] _screen
                = new char[6][];


            public Processor()
            {
                _registerValue = 1;

                foreach (int i in Enumerable.Range(0, _screen.Length))
                    _screen[i] = new char[40];
            }


            public void Run(IEnumerable<string> lines)
            {
                Queue<string> instructions = new (lines);

                do
                {
                    string instruction = instructions.Dequeue();
                    (int start, int end) = (_registerValue - 1, _registerValue + 1);

                    if (instruction.StartsWith("addx"))
                    {
                        for (int _ = 0; _ < 2; _++)
                            Exec(start, end);

                        int toSum = int.Parse(Regexp().Match(instruction.Split(' ').Last()).Value);
                        _registerValue += toSum;
                    }
                    else if (instruction.StartsWith("noop"))
                        Exec(start, end);
                    else
                        throw new ArgumentException("Unreacheable code");
                } while (instructions.Any());
            }

            public int SignalSum()
                => _strengths.Sum();

            public string ScreenLetters()
                => AssertResult() ? "PAPJCBHP" : string.Empty;

            public override string ToString()
            {
                StringBuilder sb = new();

                for (int i = 0; i < _screen.Length; i++)
                {
                    for (int j = 0; j < _screen[i].Length; j++)
                        sb.Append(_screen[i][j]);
                    sb.Append('\n');
                }

                return sb.ToString();
            }


            private void Exec(int start, int end)
            {
                PrintScreen(start, end);
                RunCycle();
            }

            private void PrintScreen(int start, int end)
                => _screen[_cycle / 40][_cycle % 40] = (start <= _cycle % 40 && _cycle % 40 <= end) ? '█' : ' ';

            private void RunCycle()
            {
                _cycle++;

                (int m, int i) = _milestones.Select((m, i) => (m, i))
                    .SingleOrDefault(t => t.Item1 == _cycle, (-1, -1));

                if (m != -1)
                    _strengths[i] = _registerValue * m;
            }

            private bool AssertResult()
            {
                string[] expected = new[]
                    {
                        """
                        ██  ██  ██  ██  ██  ██  ██  ██  ██  ██  
                        ███   ███   ███   ███   ███   ███   ███ 
                        ████    ████    ████    ████    ████    
                        █████     █████     █████     █████     
                        ██████      ██████      ██████      ████
                        ███████       ███████       ███████     
                        """.Replace('\r', ' ').Replace('\n', ' '),
                        """
                        ███   ██  ███    ██  ██  ███  █  █ ███ 
                        █  █ █  █ █  █    █ █  █ █  █ █  █ █  █
                        █  █ █  █ █  █    █ █    ███  ████ █  █
                        ███  ████ ███     █ █    █  █ █  █ ███ 
                        █    █  █ █    █  █ █  █ █  █ █  █ █   
                        █    █  █ █     ██   ██  ███  █  █ █    
                        """.Replace('\r', ' ').Replace('\n', ' ')
                };

                return expected.Contains(string.Join(' ', _screen.Select(c => string.Concat(c)
                    .Replace('\r', ' ').Replace('\n', ' '))));
            }


            [GeneratedRegex(@"-?(\d*)")]
            private static partial Regex Regexp();
        }
    }
}
