using CoreAoC.Entities;
using CoreAoC.Interfaces;

namespace CoreAoC.Engine
{
    internal class Calendar : ICalendarSolver
    {
        public IEnumerable<int> ProblemsRange { get => Enumerable.Range(1, _NUM_PROBLEMS); }

        public int Year { get; }


        private readonly IList<Problem?> _problems;
        private const int _NUM_PROBLEMS = 25;


        public Calendar(int year)
        {
            Year = year;
            _problems = new List<Problem?>(_NUM_PROBLEMS);
        }


        public void LoadData()
        {
            foreach (int day in ProblemsRange)
                _problems.Add(LoadProblem(Year, day));
        }

        public IList<Tuple<Result, Result>?> SolveAll()
            => _problems.Select((problem, day) => problem?.Solve(DataReader.ReadInput(Year, day + 1))).ToList();

        public Tuple<Result, Result>? SolveOne(int day)
            => _problems[day - 1]?.Solve(DataReader.ReadInput(Year, day));


        private static Problem? LoadProblem(int year, int day)
        {
            Type? problemType = LoadProblemType(year, day);
            return problemType == null ? null : (Problem)Activator.CreateInstance(problemType)!;
        }

        private static Type? LoadProblemType(int year, int day)
            => AppDomain.CurrentDomain.GetAssemblies()
                .SelectMany(assembly => assembly.GetTypes())
                .Where(type => type.Namespace != null && type.Namespace.Contains($"Y{year}") && type.IsSubclassOf(typeof(Problem)))
                .SingleOrDefault(type => $"P{day}".Equals(type.Name));
    }
}
