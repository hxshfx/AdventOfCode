using CoreAoC.Entities;

namespace CoreAoC.Interfaces
{
    public interface ICalendarSolver
    {
        public static IEnumerable<int> ProblemsRange
            => Enumerable.Range(1, 25);

        public int Year { get; }


        public IList<Tuple<Result, Result>> SolveAll();
        
        public Tuple<Result, Result>? SolveOne(int day);
    }
}
