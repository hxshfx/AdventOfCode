
namespace AdventOfCode.Utils
{
    internal abstract class Problem
    {
        public abstract (Part, Part) Parts { get; set; }
        public IEnumerable<string> Lines { get; }

        protected Problem(string inputPath)
            => Lines = File.ReadLines(inputPath);

        public virtual Tuple<Result, Result> Solve()
            => new(Parts.Item1.Compute(Lines), Parts.Item2.Compute(Lines));
    }
}
