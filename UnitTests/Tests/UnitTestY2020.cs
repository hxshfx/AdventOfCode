using UnitTests.Utils;
using Xunit;

namespace UnitTests.Tests
{
    public class UnitTestY2020 : YearTester
    {
        public UnitTestY2020() : base(2020) { }


        [SkippableTheory]
        [ClassData(typeof(ProblemTestCases))]
        public void TestProblems2020(string problem)
            => AssertProblem(problem);
    }
}