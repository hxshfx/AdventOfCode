using CoreAoC.Entities;
using Newtonsoft.Json.Linq;

namespace CoreAoC.Utils
{
    internal static class AssemblySearcher
    {
        public static Type? GetProblemFromYearAndDay(int year, int day)
            => GetProblemsFromYear(year).SingleOrDefault(t => t.Name.Equals($"P{day}"));

        public static IEnumerable<Type> GetProblemsFromYear(int year)
            => AppDomain.CurrentDomain
                .GetAssemblies().Single(a => a.GetName().Name!.Equals("AdventOfCode"))
                .GetTypes().Where(t => t.IsSubclassOf(typeof(Problem)) && t.Namespace!.Contains($"Y{year}"));

        public static Tuple<Result, Result> GetResultsFromProblem(JObject results)
            => new(new(results.First!.Children().Single().Value<string>()!),
                   new(results.Last!.Children().Single().Value<string>()!));

        public static IEnumerable<int> GetYearsImplemented()
            => AppDomain.CurrentDomain
                .GetAssemblies().Single(a => a.GetName().Name!.Equals("AdventOfCode"))
                .GetTypes().Where(t => t.IsSubclassOf(typeof(Problem)))
                .Select(t => int.Parse(t.Namespace!.Split('.').Last()[1..]))
                .Distinct();
    }
}
