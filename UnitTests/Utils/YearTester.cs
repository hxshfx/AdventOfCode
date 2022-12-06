using CoreAoC.Entities;
using Xunit;

namespace UnitTests.Utils
{
    public class YearTester
    {
        protected readonly IDictionary<Problem, ProblemData> _problemData;


        public YearTester(int year)
        {
            _problemData = new Dictionary<Problem, ProblemData>();

            IDictionary<string, object> rawSolutions = DataReader.ReadSolutions(year);

            IEnumerable<Type> problems = AppDomain.CurrentDomain
                .GetAssemblies().Single(a => a.GetName().Name!.Equals("AdventOfCode"))
                .GetTypes().Where(t => t.IsSubclassOf(typeof(Problem)) && t.Namespace!.Contains($"Y{year}"));

            foreach (Type problemType in problems)
            {
                Problem problem = (Problem)Activator.CreateInstance(problemType)!;

                IEnumerable<string> sample = DataReader.ReadSample(year, problemType.Name);
                Tuple<Result, Result> result = new(new(rawSolutions[problem.Parts.Item1.GetType().Name].ToString()!),
                    new(rawSolutions[problem.Parts.Item2.GetType().Name].ToString()!));

                _problemData.Add(problem, new(sample, result));
            }
        }

        protected void AssertProblem(string problem)
        {
            Skip.IfNot(_problemData.Any(kvp => problem.Equals(kvp.Key.GetType().Name)), "Problema no implementado");

            KeyValuePair<Problem, ProblemData> kvp = _problemData.Single(kvp => problem.Equals(kvp.Key.GetType().Name));

            Tuple<Result, Result> expected = kvp.Value.Result;
            Tuple<Result, Result> found = kvp.Key.Solve(kvp.Value.Sample);

            Assert.Equal(expected, found);
        }
    }
}
