using CoreAoC.Factories.Implementation;
using TestingProject.Utils;
using Xunit;

namespace TestingProject.Tests.ProblemTesters
{
    public class TesterY2020 : YearTester
    {
        public TesterY2020() :
            base(new DataReaderFactory(), 2020) { }


        [SkippableTheory]
        [ClassData(typeof(ProblemTestCases))]
        public void TestInputs2020(string problem)
            => AssertInput(problem);


        [SkippableTheory]
        [ClassData(typeof(ProblemTestCases))]
        public void TestSamples2020(string problem)
            => AssertSample(problem);
    }
}