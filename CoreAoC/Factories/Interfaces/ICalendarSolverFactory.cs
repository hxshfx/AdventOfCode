using CoreAoC.Interfaces;

namespace CoreAoC.Factories.Interfaces
{
    public interface ICalendarSolverFactory
    {
        internal IInputReader InputReader { get; }


        public ICalendarSolver Create(int year);
    }
}
