using AoC21.Utils;

Console.OutputEncoding = System.Text.Encoding.UTF8;

#pragma warning disable CS8619

IEnumerable<Type> problems = AppDomain.CurrentDomain.GetAssemblies()
    .SelectMany(assembly => assembly.GetTypes())
    .Where(type => type.IsClass && type.IsSubclassOf(typeof(Problem)));

IEnumerable<string> paths = Enumerable.Zip(Enumerable.Repeat(@".\Inputs\{0}.txt", problems.Count()), problems)
    .Select(t => string.Format(t.First, t.Second.Name));

IEnumerable<(string, string)> solutions = Enumerable.Zip(
    typeof(Solutions).GetFields().Where(fi => fi.Name.Contains("_1"))
        .Select(fi => Convert.ToString(fi.GetRawConstantValue())),
    typeof(Solutions).GetFields().Where(fi => fi.Name.Contains("_2"))
        .Select(fi => Convert.ToString(fi.GetRawConstantValue())));

#pragma warning restore CS8619

(bool, bool) correct;
object? instance;
Problem problem;
Tuple<Result, Result> results;

foreach ((Type, string, (string, string)) tuple in Enumerable.Zip(problems, paths, solutions))
{
    instance = Activator.CreateInstance(tuple.Item1, new object[] { tuple.Item2 });

    if (instance != null)
    {
        problem = (Problem)instance;
        results = problem.Solve();

        correct = (tuple.Item3.Item1 == results.Item1.Answer, tuple.Item3.Item2 == results.Item2.Answer);

        Console.WriteLine($"Problem {tuple.Item1.Name[1..]} :::::: {(correct.Item1 && correct.Item2 ? '\u2713' : '\u2A2F')}");
        Console.WriteLine($"\t=> {tuple.Item1.Name}_1 :: {(correct.Item1 ? '\u2713' : '\u2A2F')} {results.Item1}");
        Console.WriteLine($"\t=> {tuple.Item1.Name}_2 :: {(correct.Item2 ? '\u2713' : '\u2A2F')} {results.Item2}");
    }
}

