
namespace AoC21.Problems
{
    internal class P1 : Problem
    {
        public P1(string inputPath) : base(inputPath) { }

        public override string Compute()
        {
            return ComputeRecursive(Lines, 0, null).ToString();
        }

        private static int ComputeRecursive(IEnumerator<string> iter, int result, string? currentLine)
        {
            if (currentLine == null && iter.MoveNext()) currentLine = iter.Current;
            
            if (!iter.MoveNext()) return result;

            if (Convert.ToInt32(currentLine) < Convert.ToInt32(iter.Current)) result++;

            return ComputeRecursive(iter, result, iter.Current);
        }
    }
}
