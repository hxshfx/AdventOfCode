using System.Reflection;
using System.Text.RegularExpressions;

namespace CoreAoC.Entities
{
    public abstract partial class Problem
    {
        public Tuple<Part, Part> Parts { get; }


        protected Problem()
            => Parts = ActivateParts();


        public Tuple<Result, Result> Solve(IEnumerable<string> lines)
            => new(Parts.Item1.SolvePart(lines), Parts.Item2.SolvePart(lines));


        public override bool Equals(object? obj)
            => obj is Problem problem &&
               GetType().Name.Equals(problem.GetType().Name);

        public override int GetHashCode()
            => GetType().Name.GetHashCode();



        private Tuple<Part, Part> ActivateParts()
        {
            Type[] nestedParts = GetType().GetNestedTypes(BindingFlags.NonPublic);

            return new((Part)nestedParts.Single(t => RegexpP1().IsMatch(t.Name)).GetConstructors().Single().Invoke(Array.Empty<object>()),
                (Part)nestedParts.Single(t => RegexpP2().IsMatch(t.Name)).GetConstructors().Single().Invoke(Array.Empty<object>()));
        }


        [GeneratedRegex(@"P\d*_1")]
        private static partial Regex RegexpP1();

        [GeneratedRegex(@"P\d*_2")]
        private static partial Regex RegexpP2();
    }
}
