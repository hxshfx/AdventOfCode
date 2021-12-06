using AdventOfCode.Utils;

namespace AdventOfCode.Problems.Y2021
{
    internal class P1 : Problem
    {
        public override (Part, Part) Parts { get; set; }


        public P1(string inputPath) : base(inputPath)
            => Parts = (new P1_1(), new P1_2());


        internal class P1_1 : Part
        {
            public override Result Compute(IEnumerable<string> lines)
                => new(ComputeRecursive(lines.GetEnumerator(), 0, null).ToString(), Sw.ElapsedMilliseconds);

            private static int ComputeRecursive(IEnumerator<string> iter, int result, string? currentLine)
            {
                if (currentLine == null && iter.MoveNext()) currentLine = iter.Current;

                else if (!iter.MoveNext()) return result;

                if (Convert.ToInt32(currentLine) < Convert.ToInt32(iter.Current)) result++;

                return ComputeRecursive(iter, result, iter.Current);
            }
        }

        internal class P1_2 : Part
        {
            private const short WINDOW_SIZE = 3;

            public override Result Compute(IEnumerable<string> lines)
            => new(ComputeRecursive(lines.GetEnumerator(), 0, null).ToString(), Sw.ElapsedMilliseconds);

            private static int ComputeRecursive(IEnumerator<string> iter, int result, string[]? currentWindow)
            {
                if (currentWindow == null) currentWindow = GetFirstWindow(iter);

                string[] nextWindow = GetNextWindow(iter, currentWindow);

                if (nextWindow.Length == 0) return result;

                if (IsNextWindowBigger(currentWindow, nextWindow)) result++;

                return ComputeRecursive(iter, result, nextWindow);
            }

            private static string[] GetFirstWindow(IEnumerator<string> iter)
            {
                string[] window = new string[WINDOW_SIZE];

                for (int i = 0; i < WINDOW_SIZE && iter.MoveNext(); i++) window[i] = iter.Current;

                return window;
            }

            private static string[] GetNextWindow(IEnumerator<string> iter, string[] currentWindow)
            {
                if (!iter.MoveNext()) return Array.Empty<string>();

                string[] nextWindow = new string[WINDOW_SIZE]
                {
                    currentWindow[^2],
                    currentWindow[^1],
                    iter.Current
                };

                return nextWindow;
            }

            private static bool IsNextWindowBigger(string[] currentWindow, string[] nextWindow)
                => Sum(currentWindow) < Sum(nextWindow);

            private static int Sum(string[] window)
                => window.Select(s => Convert.ToInt32(s)).Sum(s => s);
        }
    }
}
