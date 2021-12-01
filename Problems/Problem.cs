
namespace AoC21.Problems
{
    internal abstract class Problem
    {
        public IEnumerator<string> Lines { get; }

        public Problem(string inputPath)
        {
            Lines = File.ReadLines(inputPath).GetEnumerator();
        }

        public abstract string Compute();
    }
}
