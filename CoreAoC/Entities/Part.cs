using System.Diagnostics;

namespace CoreAoC.Entities
{
    public abstract class Part
    {
        protected readonly Stopwatch _sw = new();


        protected Part()
            => _sw.Start();


        public Result SolvePart(IEnumerable<string> lines)
            => new(Compute(lines), _sw, GetType().Name);

        protected abstract string Compute(IEnumerable<string> lines);
    }
}
