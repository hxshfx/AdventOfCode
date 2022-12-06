using UnitTests.Utils;
using Xunit;

namespace UnitTests.Tests
{
    public class UnitTestY2022 : YearTester
    {
        public UnitTestY2022() : base(2022) { }


        [SkippableTheory]
        [ClassData(typeof(ProblemTestCases))]
        public void TestProblems2022(string problem)
            => AssertProblem(problem);
    }
}