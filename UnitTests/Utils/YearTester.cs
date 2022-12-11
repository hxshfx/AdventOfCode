using CoreAoC.Entities;
using CoreAoC.Factories.Interfaces;
using CoreAoC.Interfaces;
using Xunit;

namespace TestingProject.Utils
{
    public class YearTester
    {
        protected readonly IDictionary<Problem, IEnumerable<string>> _inputs;
        protected readonly IDictionary<Problem, IEnumerable<string>> _samples;

        protected readonly IDictionary<Problem, Tuple<Result, Result>> _inputsResults;
        protected readonly IDictionary<Problem, Tuple<Result, Result>> _sampleResults;


        public YearTester(IDataReaderFactory dataReaderFactory, int year)
        {
            IInputReader inputReader = dataReaderFactory.CreateInputReader();
            IOutputReader outputReader = dataReaderFactory.CreateOutputReader();

            _inputs = inputReader.GetInputs()[year];
            _samples = inputReader.GetSamples()[year];

            _inputsResults = outputReader.GetInputResults()[year];
            _sampleResults = outputReader.GetSampleResults()[year];
        }


        protected void AssertInput(string problem)
            => AssertProblem(_inputs, _inputsResults, problem);

        protected void AssertSample(string problem)
            => AssertProblem(_samples, _sampleResults, problem);


        private static void AssertProblem(IDictionary<Problem, IEnumerable<string>> input, IDictionary<Problem, Tuple<Result, Result>> output, string problem)
        {
            Skip.IfNot(input.Any(kvp => problem.Equals(kvp.Key.GetType().Name)) || output.Any(kvp => problem.Equals(kvp.Key.GetType().Name)), "Problem not implemented yet");

            KeyValuePair<Problem, Tuple<Result, Result>> kvp = output.Single(kvp => problem.Equals(kvp.Key.GetType().Name));
            
            Tuple<Result, Result> expected = kvp.Value;
            Tuple<Result, Result> found = kvp.Key.Solve(input[kvp.Key]);

            Assert.Equal(expected, found);
        }
    }
}
