using CoreAoC.Entities;

namespace CoreAoC.Interfaces
{
    public interface IOutputReader
    {
        public IDictionary<int, IDictionary<Problem, Tuple<Result, Result>>> GetInputResults();

        public IDictionary<int, IDictionary<Problem, Tuple<Result, Result>>> GetSampleResults();
    }
}
