using AdventOfCode.Utils;

namespace AdventOfCode.Problems.Y2020
{
    internal class P2 : Problem
    {
        public override (Part, Part) Parts { get; set; }


        public P2(string inputPath) : base(inputPath)
            => Parts = (new P2_1(), new P2_2());


        internal class P2_1 : Part
        {
            public override Result Compute(IEnumerable<string> lines)
                => new(ComputeRecursive(lines.GetEnumerator(), 0).ToString(), Sw.ElapsedMilliseconds);


            private static int ComputeRecursive(IEnumerator<string> iter, int result)
            {
                if (!iter.MoveNext()) return result;

                if (CheckIfValid(iter.Current)) result++;

                return ComputeRecursive(iter, result);
            }

            private static bool CheckIfValid(string line)
            {
                string[] split = line.Split(':');
                (int, int, char) policy = GetPolicy(split[0]);
                string password = split[1].Trim();

                return IsValid(CheckOccurrences(password, policy.Item3), policy.Item1, policy.Item2);
            }

            private static (int, int, char) GetPolicy(string line)
            {
                string[] split1 = line.Split(' ');
                string[] split2 = split1[0].Split('-');

                return (Convert.ToInt32(split2[0]), Convert.ToInt32(split2[1]), Convert.ToChar(split1[1]));
            }

            private static int CheckOccurrences(string password, char letter)
                => password.Count(s => s == letter);

            private static bool IsValid(int occurrences, int min, int max)
                => min <= occurrences && occurrences <= max;
        }

        internal class P2_2 : Part
        {
            public override Result Compute(IEnumerable<string> lines)
                => new(ComputeRecursive(lines.GetEnumerator(), 0).ToString(), Sw.ElapsedMilliseconds);


            private static int ComputeRecursive(IEnumerator<string> iter, int result)
            {
                if (!iter.MoveNext()) return result;

                if (CheckIfValid(iter.Current)) result++;

                return ComputeRecursive(iter, result);
            }

            private static bool CheckIfValid(string line)
            {
                string[] split = line.Split(':');
                (int, int, char) policy = GetPolicy(split[0]);
                string password = split[1].Trim();

                return IsValid(password, policy);
            }

            private static (int, int, char) GetPolicy(string line)
            {
                string[] split1 = line.Split(' ');
                string[] split2 = split1[0].Split('-');

                return (Convert.ToInt32(split2[0]), Convert.ToInt32(split2[1]), Convert.ToChar(split1[1]));
            }

            private static bool IsValid(string password, (int, int, char) policy)
                => password[policy.Item1 - 1] == policy.Item3 ^ password[policy.Item2 - 1] == policy.Item3;
        }
    }
}
