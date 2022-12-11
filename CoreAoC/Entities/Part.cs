using System.Diagnostics;

namespace CoreAoC.Entities
{
    public abstract class Part
    {
        public Result SolvePart(IEnumerable<string> lines)
        {
            Stopwatch sw = new();
            sw.Start();

            object result = Compute(lines);
            return new(result.ToString()!, sw, GetType().Name);
        }

        protected abstract object Compute(IEnumerable<string> lines);
    }
}
