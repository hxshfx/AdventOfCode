using AdventOfCode.Utils;

namespace AdventOfCode.Problems.Y2020
{
    internal class P1 : Problem
    {
        public override (Part, Part) Parts { get; set; }

        private const int TARGET = 2020;


        public P1(string inputPath) : base(inputPath)
            => Parts = (new P1_1(), new P1_2());


        internal class P1_1 : Part
        {
            public override Result Compute(IEnumerable<string> lines)
                => new(ComputeRecursive(lines.ToList(), 0, -1).ToString(), Sw.ElapsedMilliseconds);

            private static int ComputeRecursive(IList<string> list, int index, int result)
            {
                if (result != -1 || index >= list.Count) return (TARGET - result) * result;

                result = Match(list, index);

                return ComputeRecursive(list, ++index, result);
            }

            private static int Match(IList<string> list, int index)
                => list.Select(s => Convert.ToInt32(s))
                    .Where(n => n + Convert.ToInt32(list[index]) == TARGET).SingleOrDefault(-1);
        }

        internal class P1_2 : Part
        {
            public override Result Compute(IEnumerable<string> lines)
            => new(ComputeRecursive(lines.ToList(), 0, (-1, 0)).ToString(), Sw.ElapsedMilliseconds);

            private static int ComputeRecursive(IList<string> list, int index, (int, int) result)
            {
                if (result.Item1 != -1 || index >= list.Count) return Solve(result);

                Match(list, index, ref result);

                return ComputeRecursive(list, ++index, result);
            }

            private static void Match(IList<string> list, int index, ref (int, int) result)
            {
                int number = Convert.ToInt32(list[index]);
                IEnumerable<int> listToCheck = list.Select(i => Convert.ToInt32(i)).Where(i => i != number);

                foreach (int toCheck in listToCheck)
                {
                    if (result.Item1 != -1) break;

                    result.Item2 = Match(list, number, toCheck);

                    if (result.Item2 != -1) result.Item1 = toCheck;
                }
            }

            private static int Match(IList<string> list, int number, int toCheck)
                => list.Select(s => Convert.ToInt32(s))
                    .Where(n => n + number + toCheck == TARGET).SingleOrDefault(-1);

            private static int Solve((int, int) numbers)
                => (TARGET - numbers.Item1 - numbers.Item2) * numbers.Item1 * numbers.Item2;
        }
    }
}
