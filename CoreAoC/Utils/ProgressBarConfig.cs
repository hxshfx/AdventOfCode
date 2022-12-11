using ShellProgressBar;

namespace CoreAoC.Utils
{
    internal static class ProgressBarConfig
    {
        public static ProgressBarOptions MainOptions
            => new()
            {
                BackgroundColor = ConsoleColor.DarkGray,
                ForegroundColor = ConsoleColor.DarkCyan,
                CollapseWhenFinished = false,
                DisplayTimeInRealTime = false,
                ProgressBarOnBottom = true,
                PercentageFormat = " ⸬ ⸬ {0:F0} % completado ⸬ ⸬ ",
                ProgressCharacter = '━'
            };

        public static ProgressBarOptions ProblemPackOptions
            => new()
            {
                BackgroundColor = ConsoleColor.DarkGray,
                ForegroundColor = ConsoleColor.DarkGray,
                ForegroundColorDone = ConsoleColor.Green,
                ForegroundColorError = ConsoleColor.DarkRed,
                CollapseWhenFinished = false,
                DisplayTimeInRealTime = false,
                ProgressBarOnBottom = true,
                ProgressCharacter = '─'
            };

        public static ProgressBarOptions ProblemOptions
            => new()
            {
                BackgroundColor = ConsoleColor.DarkGray,
                ForegroundColor = ConsoleColor.DarkYellow,
                ForegroundColorDone = ConsoleColor.Green,
                ForegroundColorError = ConsoleColor.DarkRed,
                CollapseWhenFinished = false,
                DisplayTimeInRealTime = false,
                ProgressBarOnBottom = true,
                ProgressCharacter = '─'
            };

        public static ProgressBarOptions PartImplementedOptions
            => new()
            {
                ForegroundColor = ConsoleColor.DarkGreen,
                ForegroundColorError = ConsoleColor.DarkRed,
                CollapseWhenFinished = false,
                DisableBottomPercentage = true,
                DisplayTimeInRealTime = false,
                ProgressBarOnBottom = true,
                ProgressCharacter = '─'
            };

        public static ProgressBarOptions PartUnimplementedOptions
            => new()
            {
                ForegroundColor = ConsoleColor.DarkGray,
                ForegroundColorError = ConsoleColor.DarkRed,
                CollapseWhenFinished = false,
                DisableBottomPercentage = true,
                DisplayTimeInRealTime = false,
                ProgressBarOnBottom = true,
                ProgressCharacter = '─'
            };
    }
}
