using AoC21.Problems;
using System.Diagnostics;

Console.OutputEncoding = System.Text.Encoding.UTF8;

IEnumerable<Type> problems = AppDomain.CurrentDomain.GetAssemblies()
    .SelectMany(assembly => assembly.GetTypes())
    .Where(type => type.IsClass && type.IsSubclassOf(typeof(Problem)));

string[] solutions = new string[]
{
    "1676", "1706", "1698735", "1594785890", "1131506", "7863147"
};

string path = string.Empty, result = string.Empty;
char success = '\0';

Problem problem;
Stopwatch sw = new();

foreach ((Type, string) tuple in problems.Zip(solutions))
{
    path = @$".\Inputs\{tuple.Item1.Name.Split('_')[0]}.txt";
    object? instance = Activator.CreateInstance(tuple.Item1, new object[] { path });

    if (instance != null)
    {
        problem = (Problem)instance;

        sw.Start();
        result = problem.Compute();
        sw.Stop();

        success = tuple.Item2.Equals(result) ? '\u2713' : '\u2A2F';
        Console.WriteLine($"{tuple.Item1.Name} :: {success} \u2192 {result} ({sw.ElapsedMilliseconds} ms)");

        sw.Reset();
    }
}
