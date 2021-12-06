using System.Diagnostics;

namespace AdventOfCode.Utils
{
    internal abstract class Part
    {
        public Stopwatch Sw = new();

        protected Part()
            => Sw.Start();

        public abstract Result Compute(IEnumerable<string> lines);

        ~Part()
            => Sw.Stop();
    }
}
