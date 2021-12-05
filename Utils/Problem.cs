
namespace AoC21.Utils
{
    internal abstract class Problem
    {
        public abstract Part Part1 { get; set; }
        public abstract Part Part2 { get; set; }
        public IEnumerable<string> Lines { get; }

        protected Problem(string inputPath)
            => Lines = File.ReadLines(inputPath);

        public virtual Tuple<Result, Result> Solve()
            => new(Part1.Compute(Lines), Part2.Compute(Lines));
    }
}
