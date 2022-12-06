using CoreAoC.Entities;

namespace UnitTests.Utils
{
    public class ProblemData
    {
        public IEnumerable<string> Sample { get; }

        public Tuple<Result, Result> Result { get; }


        public ProblemData(IEnumerable<string> sample, Tuple<Result, Result> result)
        {
            Sample = sample;
            Result = result;
        }
    }
}
