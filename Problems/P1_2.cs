
namespace AoC21.Problems
{
    internal class P1_2 : Problem
    {
        private const short WINDOW_SIZE = 3;

        public P1_2(string inputPath) : base(inputPath) { }

        public override string Compute()
            => ComputeRecursive(Lines.GetEnumerator(), 0, null).ToString();


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
