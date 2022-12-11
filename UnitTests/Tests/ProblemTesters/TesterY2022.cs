using CoreAoC.Factories.Implementation;
using TestingProject.Utils;
using Xunit;

namespace TestingProject.Tests.ProblemTesters
{
    public class TesterY2022 : YearTester
    {
        public TesterY2022() :
            base(new DataReaderFactory(), 2022)
        { }


        [SkippableTheory]
        [ClassData(typeof(ProblemTestCases))]
        public void TestInputs2022(string problem)
            => AssertInput(problem);


        [SkippableTheory]
        [ClassData(typeof(ProblemTestCases))]
        public void TestSamples2022(string problem)
            => AssertSample(problem);
    }
}