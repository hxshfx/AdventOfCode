
namespace AoC21.Problems
{
    internal class P2_1 : Problem
    {
        public P2_1(string inputPath) : base(inputPath) { }

        public override string Compute()
            => ComputeNonRecursive(Lines.GetEnumerator()).ToString();


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
}
