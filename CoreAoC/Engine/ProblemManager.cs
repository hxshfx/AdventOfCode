using CoreAoC.Entities;
using CoreAoC.Interfaces;
using CoreAoC.Utils;
using ShellProgressBar;

namespace CoreAoC.Engine
{
    internal class ProblemManager : IProblemManager
    {
        public IEnumerable<int> YearsImplemented { get; }

        private readonly IDictionary<int, ICalendarSolver> _calendarSolvers;


        public ProblemManager(IEnumerable<int> yearsImplemented, IDictionary<int, ICalendarSolver> calendarSolvers)
        {
            YearsImplemented = yearsImplemented;
            _calendarSolvers = calendarSolvers;
        }


        public void SolveAll(IProgressBar progress)
        {
            IList<IProgressBar> yearsProgresses = YearsImplemented.Select(y
                => progress.Spawn(ICalendarSolver.ProblemsRange.Max(), $" Advent Of Code {y} ", ProgressBarConfig.ProblemPackOptions))
                    .Cast<IProgressBar>().ToList();

            Task[] tasks = new Task[yearsProgresses.Count];

            foreach ((int year, int idx) in YearsImplemented.Zip(Enumerable.Range(0, yearsProgresses.Count)))
                tasks[idx] = Task.Factory.StartNew(() => _calendarSolvers[year].SolveAll())
                    .ContinueWith(result => AllResult(progress, yearsProgresses, result.Result, idx));

            Task.WaitAll(tasks);
        }

        public void SolveCalendarYear(IProgressBar progress, int year)
        {
            IList<IProgressBar> yearProgresses = new List<IProgressBar>();
            int maxTicks = ICalendarSolver.ProblemsRange.Max() / 5;

            for (int i = 0; i < maxTicks; i++)
                yearProgresses.Add(progress.Spawn(maxTicks, $"Problemas días {i * 5:D2} - {(i + 1) * 5:D2}", ProgressBarConfig.ProblemPackOptions));

            Task[] tasks = new Task[ICalendarSolver.ProblemsRange.Count()];

            foreach (int day in ICalendarSolver.ProblemsRange)
                tasks[day - 1] = Task.Factory.StartNew(() => _calendarSolvers[year].SolveOne(day))
                    .ContinueWith(result => YearResult(progress, yearProgresses, result.Result, day));

            Task.WaitAll(tasks);
        }

        public void SolveCalendarYearAndDay(IProgressBar progress, int year, int day)
        {
            IProgressBar dayProgress = progress.Spawn(2, $" Problema {day} ", ProgressBarConfig.ProblemOptions);

            Task.Factory.StartNew(() => _calendarSolvers[year].SolveOne(day))
                .ContinueWith(task => YearAndDayResult(progress, dayProgress, task.Result)).Wait();
        }


        private static void AllResult(IProgressBar progress, IList<IProgressBar> yearProgresses, IList<Tuple<Result, Result>?> result, int idx)
        {
            if (result != null)
            {
                foreach (int _ in Enumerable.Range(0, result.Count))
                {
                    yearProgresses[idx].Tick();
                    progress.Tick();
                }

                if (1d / 3 * 100 < yearProgresses[idx].Percentage && yearProgresses[idx].Percentage < 2d / 3 * 100)
                    yearProgresses[idx].ForegroundColor = ConsoleColor.DarkYellow;
                else if (2d / 3 * 100 < yearProgresses[idx].Percentage && yearProgresses[idx].Percentage < 100)
                    yearProgresses[idx].ForegroundColor = ConsoleColor.Blue;
            }
        }

        private static void YearResult(IProgressBar progress, IList<IProgressBar> yearProgresses, Tuple<Result, Result>? result, int day)
        {
            if (result != null)
            {
                int idx = (day - 1) / 5;

                yearProgresses[idx].Tick();
                progress.Tick();

                if (yearProgresses[idx].Percentage != 100d)
                    yearProgresses[idx].ForegroundColor = ConsoleColor.DarkYellow;
            }
        }

        private static void YearAndDayResult(IProgressBar progress, IProgressBar dayProgress, Tuple<Result, Result>? result)
        {
            if (result != null)
            {
                IProgressBar barP1, barP2;

                if (!string.IsNullOrEmpty(result.Item2.Answer))
                {
                    barP2 = dayProgress.Spawn(1, $" Parte 2 ⸬ {result.Item2} ", ProgressBarConfig.PartImplementedOptions);

                    dayProgress.Tick();
                    progress.Tick();
                }
                else
                    barP2 = dayProgress.Spawn(1, $" Parte 2 sin implementar ", ProgressBarConfig.PartUnimplementedOptions);
                if (!string.IsNullOrEmpty(result.Item1.Answer))
                {
                    barP1 = dayProgress.Spawn(1, $" Parte 1 ⸬ {result.Item1} ", ProgressBarConfig.PartImplementedOptions);

                    dayProgress.Tick();
                    progress.Tick();
                }
                else
                    barP1 = dayProgress.Spawn(1, $" Parte 1 sin implementar ", ProgressBarConfig.PartUnimplementedOptions);

                barP1.Tick();
                barP2.Tick();
            }
        }
    }
}
