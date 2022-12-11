using CoreAoC.Engine;
using CoreAoC.Factories.Interfaces;
using CoreAoC.Interfaces;

namespace CoreAoC.Factories.Implementation
{
    public class ProblemManagerFactory : IProblemManagerFactory
    {
        public IDictionary<int, ICalendarSolver> CalendarSolvers { get; }
        public IEnumerable<int> YearsImplemented { get; }


        public ProblemManagerFactory(IDictionary<int, ICalendarSolver> calendarSolvers, IEnumerable<int> yearsImplemented)
        {
            CalendarSolvers = calendarSolvers;
            YearsImplemented = yearsImplemented;
        }


        public IProblemManager Create()
            => new ProblemManager(YearsImplemented, CalendarSolvers);
    }
}
