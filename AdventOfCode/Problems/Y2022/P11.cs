using CoreAoC.Entities;
using System.Runtime.CompilerServices;

[assembly: InternalsVisibleTo("TestingProject")]
namespace AdventOfCode.Problems.Y2022
{
    internal partial class P11 : Problem
    {
        internal class P11_1 : Part
        {
            private const int _N_ROUNDS = 20;

            protected override object Compute(IEnumerable<string> lines)
                => ComputeNonRecursive(lines, _N_ROUNDS, false);
        }

        internal class P11_2 : Part
        {
            private const int _N_ROUNDS = 10000;

            protected override object Compute(IEnumerable<string> lines)
                => ComputeNonRecursive(lines, _N_ROUNDS, true);
        }


        private static IEnumerable<string[]> SplitBlocks(IEnumerable<string> lines)
            => string.Join(Environment.NewLine, lines)
                        .Split(new[] { Environment.NewLine + Environment.NewLine }, StringSplitOptions.None)
                        .Select(str => str.Split(new[] { Environment.NewLine }, StringSplitOptions.None));

        private static long ComputeNonRecursive(IEnumerable<string> lines, int numberOfRounds, bool superWorried)
        {
            IList<Monkey> monkeys = new List<Monkey>();
            foreach (string[] description in SplitBlocks(lines))
                monkeys.Add(new(description));

            MonkeyTrouble game = new(monkeys, superWorried);
            foreach (int _ in Enumerable.Range(0, numberOfRounds))
                game.NewRound();

            return game.GetMonkeyBussiness();
        }


        private sealed class Monkey
        {
            public int Divisor { get; }

            private readonly Queue<long> _items;
            private readonly Func<long, long> _operation;
            private readonly Func<long, int> _throwing;


            public Monkey(string[] description)
            {
                IEnumerable<long> items = description[1].Split("Starting items: ").Last().Split(',').Select(long.Parse);
                _items = new(items);

                IEnumerable<string> operation = description[2].Split("Operation: new = old ").Last().Split(' ');
                _operation = BuildOperation(operation.First().Single(), operation.Last());

                int divisor = int.Parse(description[3].Split("Test: divisible by").Last()),
                    thrown1 = int.Parse(description[4].Split("If true: throw to monkey ").Last()),
                    thrown2 = int.Parse(description[5].Split("If false: throw to monkey ").Last());
                _throwing = n => n % divisor == 0 ? thrown1 : thrown2;

                Divisor = divisor;
            }


            public void EnqueueItem(long item)
                => _items.Enqueue(item);

            public long DequeueItem()
                => _items.Any() ? _items.Dequeue() : - 1;

            public long Operation(long worryLevel, long lcm)
            {
                long calculated = _operation.Invoke(worryLevel);
                return lcm == -1 ? (long)Math.Floor(calculated / 3d) : calculated % lcm;
            }

            public int ThrowTo(long worryLevel)
                => _throwing.Invoke(worryLevel);


            private static Func<long, long> BuildOperation(char oper, string operand)
                => oper switch
                {
                    '+' => n => n + (operand.Equals("old") ? n : long.Parse(operand)),
                    '*' => n => n * (operand.Equals("old") ? n : long.Parse(operand)),
                    _   => throw new ArgumentException("Unreachable code")
                };
        }

        private sealed class MonkeyTrouble
        {
            private readonly IDictionary<int, int> _inspectingCount;
            private readonly IList<Monkey> _monkeys;

            private readonly long _lcm;


            public MonkeyTrouble(IList<Monkey> monkeys, bool superWorried)
            {
                _monkeys = monkeys;
                _inspectingCount = Enumerable.Range(0, _monkeys.Count).ToDictionary(i => i, _ => 0);

                _lcm = superWorried ? monkeys.Select(m => m.Divisor).Aggregate(1L, (a, b) => a * b) : -1;
            }


            public void NewRound()
            {
                for (int i = 0; i < _monkeys.Count; i++)
                {
                    Monkey m = _monkeys[i];
                    long item;
                    int throwTo;

                    while ((item = m.DequeueItem()) != -1)
                    {
                        item = m.Operation(item, _lcm);
                        throwTo = m.ThrowTo(item);

                        _monkeys[throwTo].EnqueueItem(item);
                        _inspectingCount[i]++;
                    }
                }
            }

            public long GetMonkeyBussiness()
                => _inspectingCount
                    .Select(i => i.Value)
                    .OrderByDescending(i => i)
                    .Take(2)
                    .Aggregate(1L, (a, b) => a * b);
        }
    }
}
