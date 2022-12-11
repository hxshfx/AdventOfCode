using ShellProgressBar;

namespace CoreAoC.Interfaces
{
    public interface IProblemManager
    {
        public IEnumerable<int> YearsImplemented { get; }


        public void SolveAll(IProgressBar progress);

        public void SolveCalendarYear(IProgressBar progress, int year);

        public void SolveCalendarYearAndDay(IProgressBar progress, int year, int day);
    }
}
