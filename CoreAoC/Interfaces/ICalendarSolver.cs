using CoreAoC.Entities;

namespace CoreAoC.Interfaces
{
    public interface ICalendarSolver
    {
        public IEnumerable<int> ProblemsRange { get; }

        public int Year { get; }


        public IList<Tuple<Result, Result>?> SolveAll();
        
        public Tuple<Result, Result>? SolveOne(int day);
    }
}
