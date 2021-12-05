using AoC21.Utils;

namespace AoC21.Problems
{
    internal class P3 : Problem
    {
        public P3(string inputPath) : base(inputPath) { }

        public override Tuple<Result, Result> Solve()
            => new(new P3_1().Compute(Lines), new P3_2().Compute(Lines));


        internal class P3_1 : Part
        {
            public override Result Compute(IEnumerable<string> lines)
                => new(ComputeNonRecursive(lines).ToString(), Sw.ElapsedMilliseconds);


            private static int ComputeNonRecursive(IEnumerable<string> lines)
            {
                IEnumerable<int> zeroes = GetZeroes(lines, lines.First().Length);
                IEnumerable<int> ones = GetComplementary(zeroes, lines.Count());

                IEnumerable<int> gamma = GetResult(zeroes, ones);
                IEnumerable<int> epsilon = GetComplementary(gamma, 1);

                int gammaNumber = Convert.ToInt32(string.Join("", gamma), 2);
                int epsilonNumber = Convert.ToInt32(string.Join("", epsilon), 2);

                return gammaNumber * epsilonNumber;
            }

            private static IEnumerable<int> GetResult(IEnumerable<int> zeroes, IEnumerable<int> ones)
                => Enumerable.Zip(zeroes, ones).Select(x => x.First > x.Second ? 0 : 1);
        }

        internal class P3_2 : Part
        {
            public override Result Compute(IEnumerable<string> lines)
                => new(ComputeRecursive(lines).ToString(), Sw.ElapsedMilliseconds);


            private static int ComputeRecursive(IEnumerable<string> lines)
            {
                string oxygenNumber = GetOxygen(lines.ToList(), true, 0);
                string co2Number = GetOxygen(lines.ToList(), false, 0);

                return Convert.ToInt32(oxygenNumber, 2) * Convert.ToInt32(co2Number, 2);
            }

            private static string GetOxygen(IList<string> lines, bool oxygen, int step)
            {
                if (lines.Count == 1) return lines.Single();

                IEnumerable<int> zeroes = GetZeroes(lines, lines.First().Length);
                IEnumerable<int> ones = GetComplementary(zeroes, lines.Count);

                zeroes = zeroes.Skip(step);
                ones = ones.Skip(step);

                lines = lines.Select(l => l)
                    .Where(l => IsMostCommonBit(oxygen ? '1' : '0', zeroes.First(), ones.First(), l, step)).ToList();

                return GetOxygen(lines, oxygen, ++step);
            }

            private static bool IsMostCommonBit(char searched, int zeroOccurrences, int oneOccurrences, string line, int index)
                => zeroOccurrences > oneOccurrences ? line[index] == searched : line[index] != searched;
        }


        private static void Update(ref IList<int> result, string row)
        {
            for (int i = 0; i < row.Length; i++)
                if (row[i] == '0') result[i]++;
        }

        private static IEnumerable<int> GetZeroes(IEnumerable<string> rows, int length)
        {
            IList<int> result = Enumerable.Repeat(0, length).ToList();
            foreach (var row in rows) Update(ref result, row);

            return result;
        }

        private static IEnumerable<int> GetComplementary(IEnumerable<int> list, int limit)
                => list.Select(i => limit - i);
    }
}
