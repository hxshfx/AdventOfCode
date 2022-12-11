using CoreAoC.Engine;
using CoreAoC.Factories.Interfaces;
using CoreAoC.Interfaces;

namespace CoreAoC.Factories.Implementation
{
    public class CalendarSolverFactory : ICalendarSolverFactory
    {
        public IInputReader InputReader { get; }


        public CalendarSolverFactory(IInputReader inputReader)
            => InputReader = inputReader;


        public ICalendarSolver Create(int year)
            => new CalendarSolver(InputReader, year);
    }
}
