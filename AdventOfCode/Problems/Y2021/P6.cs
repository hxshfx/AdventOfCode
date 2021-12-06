using AdventOfCode.Utils;

namespace AdventOfCode.Problems.Y2021
{
    internal class P6 : Problem
    {
        public override (Part, Part) Parts { get; set; }

        private const int NEW_FISH_IDX = 8;
        private const int OLD_FISH_IDX = 6;

        public P6(string inputPath) : base(inputPath)
            => Parts = (new P6_1(), new P6_2());


        internal class P6_1 : Part
        {
            private const int DAYS = 80;

            public override Result Compute(IEnumerable<string> lines)
                => new(ComputeNonRecursive(lines, DAYS).ToString(), Sw.ElapsedMilliseconds);
        }

        internal class P6_2 : Part
        {
            private const int DAYS = 256;

            public override Result Compute(IEnumerable<string> lines)
                => new(ComputeNonRecursive(lines, DAYS).ToString(), Sw.ElapsedMilliseconds);
        }


        private static long ComputeNonRecursive(IEnumerable<string> lines, int days)
        {
            IDictionary<int, long> dict = GetInitialDict(lines.Single().Split(',').Select(i => Convert.ToInt32(i)));
            IOrderedEnumerable<KeyValuePair<int, long>> orderedDict = FillMissingValues(dict);

            for (int _ = 0; _ < days; _++)
                orderedDict = Update(orderedDict);

            return orderedDict.Select(kvp => kvp.Value).Sum();
        }

        private static IDictionary<int, long> GetInitialDict(IEnumerable<int> initialState)
            => initialState.GroupBy(i => i).OrderByDescending(i => i.Min()).ToDictionary(kvp => kvp.Key, kvp => (long)kvp.Count());

        private static IOrderedEnumerable<KeyValuePair<int, long>> FillMissingValues(IDictionary<int, long> initialDict)
        {
            Enumerable.Where(Enumerable.Range(0, NEW_FISH_IDX + 1), i => !initialDict.ContainsKey(i))
                .ToDictionary(kvp => kvp, kvp => 0).ToList().ForEach(x => initialDict.Add(x.Key, x.Value));

            return initialDict.OrderByDescending(i => i.Key);
        }

        private static IOrderedEnumerable<KeyValuePair<int, long>> Update(IOrderedEnumerable<KeyValuePair<int, long>> ordered)
            => ordered = ordered.Select(kvp => new KeyValuePair<int, long>(kvp.Key - 1, kvp.Value))
                .Append(new KeyValuePair<int, long>(OLD_FISH_IDX, ordered.ElementAt(1).Value + ordered.Last().Value))
                .Append(new KeyValuePair<int, long>(NEW_FISH_IDX, ordered.ElementAt(ordered.Count() - 1).Value))
                .Where((kvp, index) => index != 1 && index != NEW_FISH_IDX)
                .OrderByDescending(i => i.Key);
    }
}
