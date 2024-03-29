﻿using CoreAoC.Entities;
using System.Runtime.CompilerServices;

[assembly: InternalsVisibleTo("TestingProject")]
namespace AdventOfCode.Problems.Y2022
{
    internal class P6 : Problem
    {
        internal class P6_1 : Part
        {
            private const int _GROUP_SIZE = 4;

            protected override object Compute(IEnumerable<string> lines)
                => ComputeRecursive(lines.Single(), _GROUP_SIZE, 0);
        }

        internal class P6_2 : Part
        {
            private const int _GROUP_SIZE = 14;

            protected override object Compute(IEnumerable<string> lines)
                => ComputeRecursive(lines.Single(), _GROUP_SIZE, 0);
        }


        private static int ComputeRecursive(IEnumerable<char> iter, int groupSize, int result)
        {
            if (iter.Take(groupSize).Distinct().Count() == groupSize) return result + groupSize;

            else return ComputeRecursive(iter.Skip(1), groupSize, ++result);
        }
    }
}
