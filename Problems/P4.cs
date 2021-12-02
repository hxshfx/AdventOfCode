
namespace AoC21.Problems
{
    internal class P4 : Problem
    {
        public P4(string inputPath) : base(inputPath) { }

        public override string Compute()
        {
            return ComputeNonRecursive(Lines).ToString();
        }

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
