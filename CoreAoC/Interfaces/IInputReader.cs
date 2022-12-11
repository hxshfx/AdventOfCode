using CoreAoC.Entities;

namespace CoreAoC.Interfaces
{
    public interface IInputReader
    {
        public IDictionary<int, IDictionary<Problem, IEnumerable<string>>> GetInputs();

        public IDictionary<int, IDictionary<Problem, IEnumerable<string>>> GetSamples();
    }
}
