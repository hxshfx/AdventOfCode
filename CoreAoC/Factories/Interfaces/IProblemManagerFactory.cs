using CoreAoC.Interfaces;

namespace CoreAoC.Factories.Interfaces
{
    public interface IProblemManagerFactory
    {
        internal IDictionary<int, ICalendarSolver> CalendarSolvers { get; }
        internal IEnumerable<int> YearsImplemented { get; }


        public IProblemManager Create();
    }
}
