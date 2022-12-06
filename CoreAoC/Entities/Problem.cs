using System.Linq;
using System.Reflection;
using System.Text.RegularExpressions;

namespace CoreAoC.Entities
{
    public abstract class Problem
    {
        public Tuple<Part, Part> Parts { get; private set; }


        protected Problem()
            => Parts = ActivateParts();


        public Tuple<Result, Result> Solve(IEnumerable<string> lines)
            => new(Parts.Item1.SolvePart(lines), Parts.Item2.SolvePart(lines));


        private Tuple<Part, Part> ActivateParts()
        {
            Type[] nestedParts = GetType().GetNestedTypes(BindingFlags.NonPublic);

            return new((Part)nestedParts.Single(t => Regex.IsMatch(t.Name, @"P\d*_1")).GetConstructors().Single().Invoke(Array.Empty<object>()),
                (Part)nestedParts.Single(t => Regex.IsMatch(t.Name, @"P\d*_2")).GetConstructors().Single().Invoke(Array.Empty<object>()));
        }
    }
}
