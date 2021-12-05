
namespace AoC21.Utils
{
    internal class Result
    {
        public string Answer { get; set; }
        public long Elapsed { get; set; }

        public Result(string answer, long elapsed)
        {
            Answer = answer;
            Elapsed = elapsed;
        }

        public override string ToString()
            => $"{Answer} : ({Elapsed} ms)";
    }
}
