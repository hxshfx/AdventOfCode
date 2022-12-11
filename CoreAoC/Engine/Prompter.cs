using CoreAoC.Interfaces;
using CoreAoC.Utils;
using Sharprompt;
using Sharprompt.Fluent;
using ShellProgressBar;

namespace CoreAoC.Engine
{
    internal class Prompter : IPrompter
    {
        private readonly IProblemManager _manager;

        private const int _CALENDAR_DAYS = 25;


        public Prompter(IProblemManager manager)
            => _manager = manager;


        public void VisualizerThread()
        {
            EVisualizerCode code;
            int year, day;

            do
            {
                code = VisualizerPrompt();
                switch (code)
                {
                    case EVisualizerCode.EV_1:
                        SolveAll();
                        break;
                    case EVisualizerCode.EV_2:
                        year = YearPrompt(_manager.YearsImplemented);
                        SolveCalendarYear(year);
                        break;
                    case EVisualizerCode.EV_3:
                        year = YearPrompt(_manager.YearsImplemented);
                        day = DayPrompt(Enumerable.Range(1, _CALENDAR_DAYS));
                        SolveCalendarYearAndDay(year, day);
                        break;
                    default:
                        break;
                }
                Thread.Sleep(1000);
            } while (code != EVisualizerCode.EV_EXIT);
        }


        private void SolveAll()
        {
            int maxTicks = _manager.YearsImplemented.Count() * _CALENDAR_DAYS;
            int min = _manager.YearsImplemented.Min(), max = _manager.YearsImplemented.Max();

            using IProgressBar progress = new ProgressBar(maxTicks, $" Advent Of Code {min} ─ {max} ", ProgressBarConfig.MainOptions);
            _manager.SolveAll(progress);
        }

        private void SolveCalendarYear(int year)
        {
            using IProgressBar progress = new ProgressBar(_CALENDAR_DAYS, $" Advent Of Code {year} ", ProgressBarConfig.MainOptions);
            _manager.SolveCalendarYear(progress, year);
        }

        private void SolveCalendarYearAndDay(int year, int day)
        {
            using IProgressBar progress = new ProgressBar(2, $" Advent Of Code {year} (Día {day}) ", ProgressBarConfig.MainOptions);
            _manager.SolveCalendarYearAndDay(progress, year, day);
        }


        private static EVisualizerCode VisualizerPrompt()
            => Prompt.Select<EVisualizerCode>(opt => opt
                    .WithMessage("Selecciona un modo de operación")
                    .WithPageSize(Enum.GetValues<EVisualizerCode>().Length));

        private static int YearPrompt(IEnumerable<int> years)
            => Prompt.Select<int>(opt => opt
                    .WithMessage("Selecciona un año")
                    .WithPageSize(years.Count())
                    .WithItems(years));

        private static int DayPrompt(IEnumerable<int> days)
            => Prompt.Select<int>(opt => opt
                    .WithMessage("Selecciona un día")
                    .WithPageSize(days.Count())
                    .WithItems(days));
    }
}
