using AdventOfCode.Utils;
using System.Text.RegularExpressions;

namespace AdventOfCode.Problems.Y2020
{
    internal class P4 : Problem
    {
        public override (Part, Part) Parts { get; set; }

        private static readonly string[] REQUIRED_TAGS = new string[] { "byr", "ecl", "eyr", "hcl", "hgt", "iyr", "pid" };


        public P4(string inputPath) : base(inputPath)
            => Parts = (new P4_1(), new P4_2());


        internal class P4_1 : Part
        {
            public override Result Compute(IEnumerable<string> lines)
                => new(ComputeRecursive(lines.GetEnumerator(), 0, null).ToString(), Sw.ElapsedMilliseconds);

            private static int ComputeRecursive(IEnumerator<string> iter, int result, string[]? currentPassport)
            {
                if (currentPassport == null) currentPassport = GetNextPassport(iter);

                if (currentPassport.Length == 0) return result;

                if (IsValid(string.Join(" ", currentPassport).Split(' '))) result++;

                return ComputeRecursive(iter, result, GetNextPassport(iter));
            }

            private static string[] GetNextPassport(IEnumerator<string> iter)
            {
                IList<string> passport = new List<string>();

                while (iter.MoveNext() && !iter.Current.Equals(string.Empty))
                    passport.Add(iter.Current);

                return passport.ToArray();
            }

            private static bool IsValid(string[] passport)
                => Enumerable.SequenceEqual(passport.Select(p => p.Split(':')[0]).Where(p => !p.Equals("cid")).OrderBy(p => p), REQUIRED_TAGS);
        }

        internal class P4_2 : Part
        {
            private static readonly string[] VALID_ECS = new string[] { "amb", "blu", "brn", "gry", "grn", "hzl", "oth" };
            private static readonly Regex regex = new(@"(\d+)([a-zA-Z]+)");

            public override Result Compute(IEnumerable<string> lines)
                => new(ComputeRecursive(lines.GetEnumerator(), 0, null).ToString(), Sw.ElapsedMilliseconds);


            private static int ComputeRecursive(IEnumerator<string> iter, int result, string[]? currentPassport)
            {
                if (currentPassport == null) currentPassport = GetNextPassport(iter);

                if (currentPassport.Length == 0) return result;

                if (IsValid(string.Join(" ", currentPassport).Split(' '))) result++;

                return ComputeRecursive(iter, result, GetNextPassport(iter));
            }

            private static string[] GetNextPassport(IEnumerator<string> iter)
            {
                IList<string> passport = new List<string>();

                while (iter.MoveNext() && !iter.Current.Equals(string.Empty))
                    passport.Add(iter.Current);

                return passport.ToArray();
            }

            private static bool IsValid(string[] passport)
            {
                try
                {
                    bool result = Enumerable.SequenceEqual(passport.Select(p => p.Split(':')[0]).Where(p => !p.Equals("cid")).OrderBy(p => p), REQUIRED_TAGS);

                    for (int i = 0; i < passport.Length && result; i++)
                        result &= IsValidField(passport[i].Split(':'));

                    return result;
                }
                catch
                {
                    return false;
                }
            }

            private static bool IsValidField(string[] pair)
                => pair[0] switch
                {
                    "byr" => IsByrValid(Convert.ToInt32(pair[1])),
                    "ecl" => IsEclValid(pair[1]),
                    "eyr" => IsEyrValid(Convert.ToInt32(pair[1])),
                    "hcl" => IsHclValid(pair[1]),
                    "hgt" => IsHgtValid(regex.Match(pair[1])),
                    "iyr" => IsIyrValid(Convert.ToInt32(pair[1])),
                    "pid" => IsPidValud(pair[1]),
                    "cid" => true,
                    _ => false
                };

            private static bool IsByrValid(int value)
                => value.ToString().Length == 4 && 1920 <= value && value <= 2002;

            private static bool IsEclValid(string value)
                => VALID_ECS.Contains(value);

            private static bool IsIyrValid(int value)
                => value.ToString().Length == 4 && 2010 <= value && value <= 2020;

            private static bool IsEyrValid(int value)
                => value.ToString().Length == 4 && 2020 <= value && value <= 2030;

            private static bool IsHclValid(string value)
                => value[0] == '#' && int.TryParse(value[1..], System.Globalization.NumberStyles.HexNumber, null, out _);

            private static bool IsHgtValid(Match value)
                => value.Groups[2].Value.Equals("cm")
                    ? 150 <= Convert.ToInt32(value.Groups[1].Value) && Convert.ToInt32(value.Groups[1].Value) <= 193
                    : 59 <= Convert.ToInt32(value.Groups[1].Value) && Convert.ToInt32(value.Groups[1].Value) <= 76;

            private static bool IsPidValud(string value)
                => value.Length == 9 && long.TryParse(value, out _);
        }
    }
}
