using CoreAoC.Factories.Implementation;
using TestingProject.Utils;
using Xunit;

namespace TestingProject.Tests.ProblemTesters
{
    public class TesterY2021 : YearTester
    {
        public TesterY2021() :
            base(new DataReaderFactory(), 2021)
        { }


        [SkippableTheory]
        [ClassData(typeof(ProblemTestCases))]
        public void TestInputs2021(string problem)
            => AssertInput(problem);


        [SkippableTheory]
        [ClassData(typeof(ProblemTestCases))]
        public void TestSamples2021(string problem)
            => AssertSample(problem);
    }
}