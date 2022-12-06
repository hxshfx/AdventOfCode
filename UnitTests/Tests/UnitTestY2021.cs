using UnitTests.Utils;
using Xunit;

namespace UnitTests.Tests
{
    public class UnitTestY2021 : YearTester
    {
        public UnitTestY2021() : base(2021) { }


        [SkippableTheory]
        [ClassData(typeof(ProblemTestCases))]
        public void TestProblems2021(string problem)
            => AssertProblem(problem);
    }
}