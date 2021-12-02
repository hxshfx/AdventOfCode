using AoC21.Problems;
using System.Diagnostics;

IEnumerable<Type> problems = AppDomain.CurrentDomain.GetAssemblies()
    .SelectMany(assembly => assembly.GetTypes())
    .Where(type => type.IsClass && type.IsSubclassOf(typeof(Problem)));

string res = string.Empty;
Stopwatch sw = new();

foreach (var problemType in problems)
{
    object? instance = Activator.CreateInstance
        (problemType, new object[] { @$".\Inputs\{problemType.Name}.txt" });

    if (instance != null)
    {
        Problem problem = (Problem)instance;

        sw.Start();
        res = problem.Compute();
        sw.Stop();

        Console.WriteLine($"{problemType.Name} => {res} ({sw.ElapsedMilliseconds} ms)");

        sw.Reset();
    }
}
