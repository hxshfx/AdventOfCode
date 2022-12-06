using CoreAoC.Entities;
using System.Runtime.CompilerServices;

[assembly: InternalsVisibleTo("UnitTests")]
namespace AdventOfCode.Problems.Y2020
{
    internal class P8 : Problem
    {
        internal class P8_1 : Part
        {
            protected override string Compute(IEnumerable<string> lines)
                => ComputeRecursive(lines.ToList(), Enumerable.Repeat(false, lines.Count()).ToList(), 0, 0).ToString();


            private static int ComputeRecursive(IList<string> lines, IList<bool> executed, int index, int result)
            {
                if (executed[index]) return result;

                executed[index] = true;

                (int?, int?, bool) instruction = GetInstruction(lines[index]);

                if (instruction.Item1.HasValue) return ComputeRecursive(lines, executed, ++index, result + instruction.Item1.Value);

                else if (instruction.Item2.HasValue) return ComputeRecursive(lines, executed, index + instruction.Item2.Value, result);

                else return ComputeRecursive(lines, executed, ++index, result);
            }
        }

        internal class P8_2 : Part
        {
            protected override string Compute(IEnumerable<string> lines)
                => ComputeRecursive(lines.ToList(), Enumerable.Repeat(false, lines.Count()).ToList(), 0, 0).ToString();


            private static int ComputeRecursive(IList<string> lines, IList<bool> executed, int index, int result)
            {
                if (!lines[index].Contains("acc"))
                {
                    (bool, int) compute = IsFinite(lines, new List<bool>(executed), index, result, true);

                    if (compute.Item1) return compute.Item2;
                }

                executed[index] = true;

                (int?, int?, bool) instruction = GetInstruction(lines[index], false);

                if (instruction.Item1.HasValue) return ComputeRecursive(lines, executed, ++index, result + instruction.Item1.Value);

                else if (instruction.Item2.HasValue) return ComputeRecursive(lines, executed, index + instruction.Item2.Value, result);

                else return ComputeRecursive(lines, executed, ++index, result);
            }

            

            private static (bool, int) IsFinite(IList<string> lines, IList<bool> executed, int index, int result, bool swap)
            {
                if (index >= lines.Count) return (true, result);

                if (executed[index]) return (false, result);

                executed[index] = true;

                (int?, int?, bool) instruction = GetInstruction(lines[index], swap);

                if (instruction.Item3) swap = false;

                if (instruction.Item1.HasValue) return IsFinite(lines, executed, ++index, result + instruction.Item1.Value, swap);

                else if (instruction.Item2.HasValue) return IsFinite(lines, executed, index + instruction.Item2.Value, result, swap);

                else return IsFinite(lines, executed, ++index, result, swap);
            }
        }


        private static (int?, int?, bool) GetInstruction(string line, bool swap = false)
        {
            string[] split = line.Split(' ');
            (int?, int?, bool) result = (null, null, false);

            switch (split[0])
            {
                case "acc":
                    result.Item1 = Convert.ToInt32(split[1]);
                    break;
                case "jmp":
                    if (swap) result.Item3 = true;
                    else result.Item2 = Convert.ToInt32(split[1]);
                    break;
                case "nop":
                    if (swap)
                    {
                        result.Item3 = true;
                        result.Item2 = Convert.ToInt32(split[1]);
                    }
                    break;
                default:
                    break;
            }

            return result;
        }
    }
}
