
namespace AoC21.Utils
{
    internal abstract class Problem
    {
        public IEnumerable<string> Lines { get; }

        protected Problem(string inputPath)
            => Lines = File.ReadLines(inputPath);

        public abstract Tuple<Result, Result> Solve();
    }
}
