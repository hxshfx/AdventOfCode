using CoreAoC.Entities;
using System.Runtime.CompilerServices;
using System.Text.RegularExpressions;

[assembly: InternalsVisibleTo("TestingProject")]
namespace AdventOfCode.Problems.Y2020
{
    internal partial class P4 : Problem
    {
        private static readonly string[] REQUIRED_TAGS = new string[] { "byr", "ecl", "eyr", "hcl", "hgt", "iyr", "pid" };


        internal class P4_1 : Part
        {
            protected override object Compute(IEnumerable<string> lines)
                => ComputeRecursive(lines.GetEnumerator(), 0, null, IsValid);


            private static bool IsValid(string[] passport)
                => passport.Select(p => p.Split(':')[0]).Where(p => !p.Equals("cid")).OrderBy(p => p).SequenceEqual(REQUIRED_TAGS);
        }

        internal partial class P4_2 : Part
        {
            private static readonly string[] VALID_ECS = new string[] { "amb", "blu", "brn", "gry", "grn", "hzl", "oth" };


            protected override object Compute(IEnumerable<string> lines)
                => ComputeRecursive(lines.GetEnumerator(), 0, null, IsValid);


            private static bool IsValid(string[] passport)
            {
                try
                {
                    bool result = passport.Select(p => p.Split(':')[0]).Where(p => !p.Equals("cid")).OrderBy(p => p).SequenceEqual(REQUIRED_TAGS);

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
                    "hgt" => IsHgtValid(Regexp().Match(pair[1])),
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


            [GeneratedRegex("(\\d+)([a-zA-Z]+)")]
            private static partial Regex Regexp();
        }


        private static int ComputeRecursive(IEnumerator<string> iter, int result, string[]? currentPassport, Func<string[], bool> checker)
        {
            currentPassport ??= GetNextPassport(iter);

            if (currentPassport.Length == 0) return result;

            if (checker.Invoke(string.Join(" ", currentPassport).Split(' '))) result++;

            return ComputeRecursive(iter, result, GetNextPassport(iter), checker);
        }

        private static string[] GetNextPassport(IEnumerator<string> iter)
        {
            IList<string> passport = new List<string>();

            while (iter.MoveNext() && !iter.Current.Equals(string.Empty))
                passport.Add(iter.Current);

            return passport.ToArray();
        }
    }
}
