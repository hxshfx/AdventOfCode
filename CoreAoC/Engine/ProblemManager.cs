using CoreAoC.Entities;
using CoreAoC.Interfaces;
using CoreAoC.Utils;
using ShellProgressBar;

namespace CoreAoC.Engine
{
    public class ProblemManager : IDisposable
    {
        public ICalendarSolver CalendarSolver { get; set; }

        private readonly IDictionary<string, object> _solutions;
        private readonly IProgressBar _yearProgress;


        public ProblemManager(ICalendarSolver calendarSolver, IProgressBar progress)
        {
            CalendarSolver = calendarSolver;

            _solutions = DataReader.ReadSolutions(CalendarSolver.Year);
            _yearProgress = progress.Spawn(calendarSolver.ProblemsRange.Count(), $" {CalendarSolver.Year} ", ProgressBarConfig.MainOptions);
        }


        public void CalendarReport()
        {
            Task[] tasks = new Task[CalendarSolver.ProblemsRange.Count()];

            foreach (int day in CalendarSolver.ProblemsRange)
                tasks[day - 1] = Task.Factory.StartNew(() => CalendarSolver.SolveOne(day))
                    .ContinueWith((task) => ProgressChild(day, task.Result));

            Task.WaitAll(tasks.ToArray());
        }


        public void Dispose()
        {
            Dispose(true);
            GC.SuppressFinalize(this);
        }

        protected virtual void Dispose(bool disposing)
            => _yearProgress.Dispose();


        private void ProgressChild(int day, Tuple<Result, Result>? result)
        {
            IProgressBar dayBar = _yearProgress.Spawn(2, $"Día {day}", ProgressBarConfig.MainOptions);

            if (result == null)
                dayBar.Spawn(1, " sin hacer", ProgressBarConfig.ProblemUnimplementedOptions).Tick();
            else if (IsNotSolvedYet(_solutions, result))
                dayBar.Spawn(1, " doin", ProgressBarConfig.ProblemWorkingOptions).Tick();
            else if (!IsResultCorrect(_solutions, result))
                dayBar.Spawn(1, " ERROR ERROR", ProgressBarConfig.ProblemIncorrectOptions).Tick();
            else
            {
                dayBar.Spawn(1, result.Item1.ToString(), ProgressBarConfig.ProblemCorrectOptions).Tick();
                dayBar.Spawn(1, result.Item2.ToString(), ProgressBarConfig.ProblemCorrectOptions).Tick();
            }

            dayBar.Tick(2);
        }

        private static bool IsNotSolvedYet(IDictionary<string, object> solutions, Tuple<Result, Result> result)
            => !solutions.ContainsKey(result.Item1.PartName!) || !solutions.ContainsKey(result.Item2.PartName!);

        private static bool IsResultCorrect(IDictionary<string, object> solutions, Tuple<Result, Result> result)
            => solutions[result.Item1.PartName!].Equals(result.Item1.Answer) && solutions[result.Item2.PartName!].Equals(result.Item2.Answer);
    }
}
