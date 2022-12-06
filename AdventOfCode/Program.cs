using CoreAoC.Engine;
using CoreAoC.Factories;
using CoreAoC.Interfaces;
using ShellProgressBar;

ICalendarSolverFactory calendarFactory = new CalendarSolverFactory();
ICalendarSolver calendar = calendarFactory.Create(2022);

ProgressBarOptions options = new()
{
    BackgroundColor = ConsoleColor.DarkCyan,
    ForegroundColor = ConsoleColor.DarkCyan,
    CollapseWhenFinished = false,
    DenseProgressBar = true,
    DisplayTimeInRealTime = false,
    ProgressCharacter = '─',
    ProgressBarOnBottom = true
};

using ProgressBar bar = new(3, $"Advent Of Code", options);
using (ProblemManager manager = new(calendar, bar))
{
    manager.CalendarReport();
}

Thread.Sleep(Timeout.Infinite);
