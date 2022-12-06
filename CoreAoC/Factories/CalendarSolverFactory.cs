using CoreAoC.Engine;
using CoreAoC.Interfaces;

namespace CoreAoC.Factories
{
    public class CalendarSolverFactory : ICalendarSolverFactory
    {
        public ICalendarSolver Create(int year)
        {
            Calendar calendar = new(year);
            calendar.LoadData();

            return calendar;
        }
    }
}
