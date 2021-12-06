using AdventOfCode.Utils;

namespace AdventOfCode.Problems.Y2020
{
    internal class P7 : Problem
    {
        public override (Part, Part) Parts { get; set; }

        private readonly static Bag SHINY_BAG = new("shiny gold");


        public P7(string inputPath) : base(inputPath)
            => Parts = (new P7_1(), new P7_2());


        internal class P7_1 : Part
        {
            public override Result Compute(IEnumerable<string> lines)
                => new(ComputeRecursive(lines.GetEnumerator()).ToString(), Sw.ElapsedMilliseconds);


            private static int ComputeRecursive(IEnumerator<string> iter)
            {
                IEnumerable<Rule> ruleSet = GetRuleSet(iter);
                IEnumerable<Bag> bagSet = GetBagSet(ruleSet);

                return HowManyCanContain(ruleSet, bagSet, new List<Bag>());
            }

            private static IEnumerable<Rule> GetRuleSet(IEnumerator<string> iter)
            {
                IList<Rule> ruleSet = new List<Rule>();

                while (iter.MoveNext())
                    ruleSet.Add(new Rule(iter.Current));

                return ruleSet;
            }

            private static IEnumerable<Bag> GetBagSet(IEnumerable<Rule> ruleSet)
            {
                IEnumerable<Bag> bagSet = new List<Bag>();

                foreach (Rule rule in ruleSet)
                    bagSet = bagSet.Union(rule.GetDistinctBags());

                return bagSet;
            }

            private static int HowManyCanContain(IEnumerable<Rule> ruleSet, IEnumerable<Bag> bagSet, IEnumerable<Bag> found)
            {
                int result = 0;

                foreach (Bag bag in bagSet)
                {
                    if (BagContainsShinyBag(ruleSet, found, bag, false))
                    {
                        result++;
                        found = found.Append(bag);
                    }
                }

                return result - 1;
            }

            private static bool BagContainsShinyBag(IEnumerable<Rule> ruleSet, IEnumerable<Bag> found, Bag bag, bool result)
            {
                if (SHINY_BAG.Equals(bag) || found.Contains(bag)) return true;

                Rule rule = ruleSet.Select(r => r).Single(r => r.Key.Equals(bag));

                if (Enumerable.Empty<(int, Bag)>().Equals(rule.Values)) return false;

                IEnumerable<Bag> bags = rule.Values.Select(t => t.Item2);

                for (int i = 0; i < bags.Count() && !result; i++)
                    result |= BagContainsShinyBag(ruleSet, found, bags.ElementAt(i), result);

                return result;
            }
        }

        internal class P7_2 : Part
        {
            public override Result Compute(IEnumerable<string> lines)
                => new(ComputeRecursive(lines.GetEnumerator()).ToString(), Sw.ElapsedMilliseconds);


            private static int ComputeRecursive(IEnumerator<string> iter)
                => HowManyMustContain(GetRuleSet(iter));

            private static IEnumerable<Rule> GetRuleSet(IEnumerator<string> iter)
            {
                IList<Rule> ruleSet = new List<Rule>();

                while (iter.MoveNext())
                    ruleSet.Add(new Rule(iter.Current));

                return ruleSet;
            }

            private static int HowManyMustContain(IEnumerable<Rule> ruleSet)
                => GetNestedBags(ruleSet, GetShinyRule(ruleSet), 0);

            private static int GetNestedBags(IEnumerable<Rule> ruleSet, Rule rule, int result)
            {
                if (!rule.Values.Any()) return 1;

                foreach ((int, Bag) pair in rule.Values)
                    result += pair.Item1 * GetNestedBags(ruleSet, ruleSet.Select(r => r).Single(r => r.Key.Equals(pair.Item2)), 1);

                return result;
            }

            private static Rule GetShinyRule(IEnumerable<Rule> ruleSet)
                => ruleSet.Select(r => r).Single(r => SHINY_BAG.Equals(r.Key));
        }


        private class Bag
        {
            public string Colour { get; set; }

            public Bag(string colour)
            {
                Colour = colour;
            }

            public override bool Equals(object? obj)
                => obj is Bag bag && Colour.Equals(bag.Colour);

            public override int GetHashCode()
                => HashCode.Combine(Colour);

            public override string ToString()
                => Colour;
        }

        private class Rule
        {
            public Bag Key { get; set; }
            public IEnumerable<(int, Bag)> Values { get; set; }

            public Rule(string line)
            {
                Key = new(string.Join(' ', line.Split(' ')[0..2]));
                Values = line.Split("contain")[^1].Split(',')
                    .Where(l => !l.Contains(" no "))
                    .Select(l =>
                    {
                        string[] split = l.Trim().Split(' ');
                        return (Convert.ToInt32(split[0]), new Bag(string.Join(' ', split[1..3])));
                    });
            }

            public IEnumerable<Bag> GetDistinctBags()
                => Values.Select(t => t.Item2).Append(Key).Distinct();
        }
    }
}
