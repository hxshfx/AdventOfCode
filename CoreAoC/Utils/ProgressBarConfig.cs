using ShellProgressBar;

namespace CoreAoC.Utils
{
    internal static class ProgressBarConfig
    {
        public static ProgressBarOptions MainOptions
            => new()
            {
                BackgroundColor = ConsoleColor.DarkCyan,
                ForegroundColor = ConsoleColor.DarkCyan,
                CollapseWhenFinished = false,
                DenseProgressBar = true,
                DisplayTimeInRealTime = false,
                ProgressCharacter = '─',
                ProgressBarOnBottom = true
            };

        public static ProgressBarOptions ProblemCorrectOptions
            => new()
            {
                BackgroundColor = ConsoleColor.Green,
                ForegroundColor = ConsoleColor.Green,
                CollapseWhenFinished = false,
                DenseProgressBar = true,
                DisplayTimeInRealTime = false,
                ProgressCharacter = '─',
                ProgressBarOnBottom = false
            };

        public static ProgressBarOptions ProblemIncorrectOptions
            => new()
            {
                BackgroundColor = ConsoleColor.Red,
                ForegroundColor = ConsoleColor.Red,
                CollapseWhenFinished = false,
                DenseProgressBar = true,
                DisplayTimeInRealTime = false,
                ProgressCharacter = '─',
                ProgressBarOnBottom = false
            };

        public static ProgressBarOptions ProblemUnimplementedOptions
            => new()
            {
                BackgroundColor = ConsoleColor.DarkGray,
                ForegroundColor = ConsoleColor.DarkGray,
                CollapseWhenFinished = false,
                DenseProgressBar = true,
                DisplayTimeInRealTime = false,
                ProgressCharacter = '─',
                ProgressBarOnBottom = false
            };

        public static ProgressBarOptions ProblemWorkingOptions
            => new()
            {
                BackgroundColor = ConsoleColor.DarkYellow,
                ForegroundColor = ConsoleColor.DarkYellow,
                CollapseWhenFinished = false,
                DenseProgressBar = true,
                DisplayTimeInRealTime = false,
                ProgressCharacter = '─',
                ProgressBarOnBottom = false
            };
    }
}
