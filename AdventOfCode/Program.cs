using AdventOfCode.Utils;

#pragma warning disable CS8619

Console.OutputEncoding = System.Text.Encoding.UTF8;

(bool, bool) correct;
object? instance;
Problem problem;
Tuple<Result, Result> results;

IEnumerable<string> years = AppDomain.CurrentDomain.GetAssemblies()
    .SelectMany(assembly => assembly.GetTypes().Select(t => t.Namespace))
    .Where(name => name != null && name.Contains('Y')).Distinct().ToList();

foreach (string year in years)
{
    Type solutionType = typeof(Solutions).GetNestedTypes()
        .Single(type => type.Name.Contains(year.Split('.')[^1]));

    IEnumerable<Type> problems = AppDomain.CurrentDomain.GetAssemblies()
        .SelectMany(assembly => assembly.GetTypes())
        .Where(type => year.Equals(type.Namespace)&& type.IsSubclassOf(typeof(Problem)));

    IEnumerable<string> paths = Enumerable.Zip(Enumerable.Repeat(@".\Inputs\{0}\{1}.txt", problems.Count()), problems)
        .Select(path => string.Format(path.First, year.Split('.')[^1], path.Second.Name));

    IEnumerable<(string, string)> solutions = Enumerable.Zip(
        solutionType.GetFields().Where(fi => fi.Name.Contains("_1"))
            .Select(fi => Convert.ToString(fi.GetRawConstantValue())),
        solutionType.GetFields().Where(fi => fi.Name.Contains("_2"))
            .Select(fi => Convert.ToString(fi.GetRawConstantValue())));

    Console.WriteLine(string.Concat(Enumerable.Repeat(".·:·.", 10)));
    Console.WriteLine($"\t\tAdvent Of Code {year.Split('.')[^1][1..]}");
    Console.WriteLine(string.Concat(Enumerable.Repeat(".·:·.", 10)));
    Console.WriteLine("\n\n");

    foreach ((Type, string, (string, string)) tuple in Enumerable.Zip(problems, paths, solutions))
    {
        instance = Activator.CreateInstance(tuple.Item1, new object[] { tuple.Item2 });

        if (instance != null)
        {
            problem = (Problem)instance;
            results = problem.Solve();

            correct = (tuple.Item3.Item1 == results.Item1.Answer, tuple.Item3.Item2 == results.Item2.Answer);

            Console.WriteLine(string.Concat(Enumerable.Repeat("_-", 15)));
            Console.WriteLine($"Problem {tuple.Item1.Name[1..]} :: {(correct.Item1 && correct.Item2 ? '\u2713' : '\u2A2F')}");
            Console.WriteLine($"\t=> {tuple.Item1.Name}_1 :: {(correct.Item1 ? '\u2713' : '\u2A2F')}");
            Console.WriteLine($"\t\t=> {results.Item1.Answer} - {results.Item1.Elapsed} ms");
            Console.WriteLine($"\t=> {tuple.Item1.Name}_2 :: {(correct.Item2 ? '\u2713' : '\u2A2F')}");
            Console.WriteLine($"\t\t=> {results.Item2.Answer} - {results.Item2.Elapsed - results.Item1.Elapsed} ms");
        }
    }

    Console.WriteLine("\n\n");
}

#pragma warning restore CS8619
