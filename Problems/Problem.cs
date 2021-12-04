
namespace AoC21.Problems
{
    internal abstract class Problem
    {
        public IEnumerable<string> Lines { get; }

        protected Problem(string inputPath)
        {
            Lines = File.ReadLines(inputPath);
        }

        public abstract string Compute();
    }
}
