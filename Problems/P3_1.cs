
namespace AoC21.Problems
{
    internal class P3_1 : Problem
    {
        public P3_1(string inputPath) : base(inputPath) { }

        public override string Compute()
            => ComputeNonRecursive(Lines).ToString();


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

        private static IEnumerable<int> GetZeroes(IEnumerable<string> rows, int length)
        {
            IList<int> result = Enumerable.Repeat(0, length).ToList();
            foreach (var row in rows) Update(ref result, row);

            return result;
        }

        private static IEnumerable<int> GetComplementary(IEnumerable<int> list, int limit)
            => list.Select(i => limit - i);

        private static IEnumerable<int> GetResult(IEnumerable<int> zeroes, IEnumerable<int> ones)
            => Enumerable.Zip(zeroes, ones).Select(x => x.First > x.Second ? 0 : 1);

        private static void Update(ref IList<int> result, string row)
        {
            for (int i = 0; i < row.Length; i++)
                if (row[i] == '0') result[i]++;
        }
    }
}
