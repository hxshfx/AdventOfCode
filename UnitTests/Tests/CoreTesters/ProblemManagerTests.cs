using CoreAoC.Entities;
using CoreAoC.Factories.Implementation;
using CoreAoC.Factories.Interfaces;
using CoreAoC.Interfaces;
using ShellProgressBar;
using TestingProject.Utils;
using Xunit;

namespace TestingProject.Tests.CoreTesters
{
    public class ProblemManagerTests : CoreTesters<IProblemManagerFactory>
    {
        public ProblemManagerTests()
            : base(new AppHostFactory()) { }


        [Fact]
        public void TestCreateProblemManager()
            => Assert.NotNull(_service.Create());


        [Fact]
        public void TestYearsImplemented()
            => Assert.NotEmpty(_service.Create().YearsImplemented);

        [Fact]
        public void TestSolveAll()
            => Assert.Null(Record.Exception(() => _service.Create().SolveAll(MockProgressBar())));

        [Theory]
        [MemberData(nameof(YearsImplemented))]
        public void TestSolveCalendarYear(int year)
            => Assert.Null(Record.Exception(() => _service.Create().SolveCalendarYear(MockProgressBar(), year)));

        [Theory]
        [MemberData(nameof(YearsImplemented))]
        public void TestSolveOne(int year)
        {
            IProblemManager problemManager = _service.Create();

            foreach (int day in ICalendarSolver.ProblemsRange)
                Assert.Null(Record.Exception(() => _service.Create().SolveCalendarYearAndDay(MockProgressBar(), year, day)));
        }


        public static IEnumerable<object[]> YearsImplemented
            => new List<object[]>(
                AppDomain.CurrentDomain
                    .GetAssemblies().Single(a => a.GetName().Name!.Equals("AdventOfCode"))
                    .GetTypes().Where(t => t.IsSubclassOf(typeof(Problem)))
                    .Select(t => int.Parse(t.Namespace!.Split('.').Last()[1..]))
                    .Distinct()
                    .Select(year => new object[] { year })
                );

        private static IProgressBar MockProgressBar()
            => new ProgressBar(default, default, ConsoleColor.Black);
    }
}
