
namespace AdventOfCode.Utils
{
    internal class Result
    {
        public string Answer { get; set; }
        public long? Elapsed { get; set; }

        public Result(string answer)
            => Answer = answer;

        public Result(string answer, long elapsed)
        {
            Answer = answer;
            Elapsed = elapsed;
        }
    }
}
