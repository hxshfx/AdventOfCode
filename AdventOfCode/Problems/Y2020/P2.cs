using CoreAoC.Entities;
using System.Runtime.CompilerServices;

[assembly: InternalsVisibleTo("TestingProject")]
namespace AdventOfCode.Problems.Y2020
{
    internal class P2 : Problem
    {
        internal class P2_1 : Part
        {
            protected override object Compute(IEnumerable<string> lines)
                => ComputeRecursive(lines.GetEnumerator(), 0, CheckIfValid);


            private static bool CheckIfValid(string line)
            {
                string[] split = line.Split(':');
                (int, int, char) policy = GetPolicy(split[0]);
                string password = split[1].Trim();

                return IsValid(CheckOccurrences(password, policy.Item3), policy.Item1, policy.Item2);
            }

            private static int CheckOccurrences(string password, char letter)
                => password.Count(s => s == letter);

            private static bool IsValid(int occurrences, int min, int max)
                => min <= occurrences && occurrences <= max;
        }

        internal class P2_2 : Part
        {
            protected override object Compute(IEnumerable<string> lines)
                => ComputeRecursive(lines.GetEnumerator(), 0, CheckIfValid);
            

            private static bool CheckIfValid(string line)
            {
                string[] split = line.Split(':');
                (int, int, char) policy = GetPolicy(split[0]);
                string password = split[1].Trim();

                return IsValid(password, policy);
            }

            private static bool IsValid(string password, (int, int, char) policy)
                => password[policy.Item1 - 1] == policy.Item3 ^ password[policy.Item2 - 1] == policy.Item3;
        }


        private static int ComputeRecursive(IEnumerator<string> iter, int result, Func<string, bool> checker)
        {
            if (!iter.MoveNext()) return result;

            if (checker.Invoke(iter.Current)) result++;

            return ComputeRecursive(iter, result, checker);
        }

        private static (int, int, char) GetPolicy(string line)
        {
            string[] split1 = line.Split(' ');
            string[] split2 = split1[0].Split('-');

            return (Convert.ToInt32(split2[0]), Convert.ToInt32(split2[1]), Convert.ToChar(split1[1]));
        }
    }
}
