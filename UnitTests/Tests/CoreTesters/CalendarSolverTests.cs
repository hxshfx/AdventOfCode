using CoreAoC.Entities;
using CoreAoC.Factories.Implementation;
using CoreAoC.Factories.Interfaces;
using CoreAoC.Interfaces;
using TestingProject.Utils;
using Xunit;

namespace TestingProject.Tests.CoreTesters
{
    public class CalendarSolverTests : CoreTesters<ICalendarSolverFactory>
    {
        public CalendarSolverTests()
            : base(new AppHostFactory()) { }


        [Theory]
        [MemberData(nameof(YearsImplemented))]
        public void TestCreateCalendarSolver(int year)
            => Assert.NotNull(_service.Create(year));


        [Theory]
        [MemberData(nameof(YearsImplemented))]
        public void TestCalendarSolverProperties(int year)
            => Assert.Multiple(
                    () => Assert.Equal(year, _service.Create(year).Year),

                    () => Assert.Equal(25, ICalendarSolver.ProblemsRange.Max()),
                    () => Assert.True(ICalendarSolver.ProblemsRange.Max() % 5 == 0)
               );

        [Theory]
        [MemberData(nameof(YearsImplemented))]
        public void TestSolveAll(int year)
        {
            ICalendarSolver calendarSolver = _service.Create(year);
            IList<Tuple<Result, Result>> solveAll = calendarSolver.SolveAll();

            Assert.All(solveAll, results =>
                Assert.Multiple(
                    () => Assert.NotNull(results.Item1),
                    () => Assert.NotNull(results.Item2),

                    () => Assert.IsAssignableFrom<Result>(results.Item1),
                    () => Assert.IsAssignableFrom<Result>(results.Item2),

                    () => Assert.False(string.IsNullOrEmpty(results.Item1.Answer)),
                    () => Assert.False(string.IsNullOrEmpty(results.Item2.Answer))
                )
            );
        }

        [Theory]
        [MemberData(nameof(YearsImplemented))]
        public void TestSolveOne(int year)
        {
            ICalendarSolver calendarSolver = _service.Create(year);

            foreach (int day in ICalendarSolver.ProblemsRange)
            {
                Tuple<Result, Result>? solveOne = calendarSolver.SolveOne(day);

                if (solveOne == null)
                    Assert.Multiple(
                        () => Assert.False(File.Exists(string.Format(@".\Resources\Inputs\{0}\P{1}.txt", year, day))),
                        () => Assert.False(File.Exists(string.Format(@".\Resources\Samples\{0}\P{1}.txt", year, day)))
                    );
                else
                    Assert.Multiple(
                        () => Assert.NotNull(solveOne.Item1),
                        () => Assert.NotNull(solveOne.Item2),

                        () => Assert.IsAssignableFrom<Result>(solveOne.Item1),
                        () => Assert.IsAssignableFrom<Result>(solveOne.Item2),

                        () => Assert.False(string.IsNullOrEmpty(solveOne.Item1.Answer)),
                        () => Assert.False(string.IsNullOrEmpty(solveOne.Item2.Answer))
                    );
            }
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
    }
}
