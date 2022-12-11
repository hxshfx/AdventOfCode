using System.Diagnostics;

namespace CoreAoC.Entities
{
    public class Result
    {
        public string Answer { get; set; }

        public long? Elapsed { get; set; }

        public string? PartName { get; set; }


        public Result(string answer)
            => Answer = answer;

        public Result(string answer, Stopwatch sw, string partName)
        {
            Answer = answer;
            Elapsed = sw.ElapsedMilliseconds;
            PartName = partName;
        }


        public override bool Equals(object? obj)
            => obj is Result result && Answer.Equals(result.Answer);

        public override int GetHashCode()
            => HashCode.Combine(Answer);

        public override string ToString()
            => Elapsed.HasValue ? $"{Answer} @ {Elapsed} ms" : Answer;
    }
}
