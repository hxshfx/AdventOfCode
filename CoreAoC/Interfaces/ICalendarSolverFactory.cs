namespace CoreAoC.Interfaces
{
    public interface ICalendarSolverFactory
    {
        public ICalendarSolver Create(int year);
    }
}
