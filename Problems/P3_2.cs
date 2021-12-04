
namespace AoC21.Problems
{
    internal class P3_2 : Problem
    {
        public P3_2(string inputPath) : base(inputPath) { }

        public override string Compute()
            => ComputeRecursive(Lines).ToString();


        private static int ComputeRecursive(IEnumerable<string> lines)
        {
            string oxygenNumber = GetOxygen(lines.ToList(), true, 0);
            string co2Number = GetOxygen(lines.ToList(), false, 0);

            return Convert.ToInt32(oxygenNumber, 2) * Convert.ToInt32(co2Number, 2);
        }

        private static IEnumerable<int> GetZeroes(IEnumerable<string> rows, int length)
        {
            IList<int> result = Enumerable.Repeat(0, length).ToList();
            foreach (var row in rows) Update(ref result, row);

            return result;
        }

        private static void Update(ref IList<int> result, string row)
        {
            for (int i = 0; i < row.Length; i++)
                if (row[i] == '0') result[i]++;
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

        private static IEnumerable<int> GetComplementary(IEnumerable<int> list, int limit)
            => list.Select(i => limit - i);

        private static bool IsMostCommonBit(char searched, int zeroOccurrences, int oneOccurrences, string line, int index)
            => zeroOccurrences > oneOccurrences ? line[index] == searched : line[index] != searched;
    }
}
