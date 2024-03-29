﻿using CoreAoC.Entities;
using System.Runtime.CompilerServices;

[assembly: InternalsVisibleTo("TestingProject")]
namespace AdventOfCode.Problems.Y2021
{
    internal class P2 : Problem
    {
        internal class P2_1 : Part
        {
            protected override object Compute(IEnumerable<string> lines)
                => ComputeNonRecursive(lines.GetEnumerator());


            private static int ComputeNonRecursive(IEnumerator<string> iter)
            {
                (int, int) result = (0, 0);

                while (iter.MoveNext())
                {
                    string[] split = iter.Current.Split(' ');
                    result = GetNewPosition(split, result);
                }

                return GetMultipliedPosition(result);
            }

            private static (int, int) GetNewPosition(string[] line, (int, int) position)
            {
                if ("forward".Equals(line[0])) position.Item1 += Convert.ToInt32(line[1]);
                else if ("up".Equals(line[0])) position.Item2 -= Convert.ToInt32(line[1]);
                else if ("down".Equals(line[0])) position.Item2 += Convert.ToInt32(line[1]);

                return position;
            }

            private static int GetMultipliedPosition((int, int) position)
                => position.Item1 * position.Item2;
        }

        internal class P2_2 : Part
        {
            protected override object Compute(IEnumerable<string> lines)
                => ComputeNonRecursive(lines.GetEnumerator());


            private static int ComputeNonRecursive(IEnumerator<string> iter)
            {
                (int, int, int) result = (0, 0, 0);

                while (iter.MoveNext())
                {
                    string[] split = iter.Current.Split(' ');
                    result = GetNewPosition(split, result);
                }

                return GetMultipliedPosition(result);
            }

            private static (int, int, int) GetNewPosition(string[] line, (int, int, int) position)
                => "forward".Equals(line[0]) ? MoveForward(line, position) : MoveUpAndDown(line, position);

            private static (int, int, int) MoveForward(string[] line, (int, int, int) position)
            {
                int x = Convert.ToInt32(line[1]);

                position.Item1 += x;
                position.Item2 += x * position.Item3;

                return position;
            }

            private static (int, int, int) MoveUpAndDown(string[] line, (int, int, int) position)
            {
                if ("up".Equals(line[0])) position.Item3 -= Convert.ToInt32(line[1]);
                else if ("down".Equals(line[0])) position.Item3 += Convert.ToInt32(line[1]);

                return position;
            }

            private static int GetMultipliedPosition((int, int, int) position)
                => position.Item1 * position.Item2;
        }
    }
}
