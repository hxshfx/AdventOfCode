using CoreAoC.Factories.Implementation;
using CoreAoC.Factories.Interfaces;
using CoreAoC.Interfaces;
using CoreAoC.Utils;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;

namespace CoreAoC.Hosting
{
    internal static class HostBuilder
    {
        public static IHost Build() 
            => Host.CreateDefaultBuilder()
                .ConfigureServices((hostBuilder, services) =>
                {
                    IEnumerable<int> yearsImplemented = AssemblySearcher.GetYearsImplemented();

                    services.AddSingleton<IDataReaderFactory, DataReaderFactory>();
                    services.AddSingleton<ICalendarSolverFactory, CalendarSolverFactory>(provider =>
                        new(provider.GetRequiredService<IDataReaderFactory>().CreateInputReader()));

                    services.AddSingleton<IProblemManagerFactory, ProblemManagerFactory>(provider =>
                    {
                        ICalendarSolverFactory calendarFactory = provider.GetRequiredService<ICalendarSolverFactory>();
                        IDictionary<int, ICalendarSolver> calendarSolvers = new Dictionary<int, ICalendarSolver>(yearsImplemented
                            .Select(y => calendarFactory.Create(y)).ToDictionary(y => y.Year));

                        return new(calendarSolvers, yearsImplemented);
                    });

                    services.AddSingleton<IPrompterFactory, PrompterFactory>(provider =>
                        new(provider.GetRequiredService<IProblemManagerFactory>().Create()));
                })
                .ConfigureLogging(loggingBuilder => loggingBuilder.ClearProviders())
                .Build();
    }
}
