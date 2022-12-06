using System.Collections;

namespace UnitTests.Utils
{
    internal class ProblemTestCases : IEnumerable<object[]>
    {
        public IEnumerator<object[]> GetEnumerator()
        {
            yield return new object[] { "P1" };
            yield return new object[] { "P2" };
            yield return new object[] { "P3" };
            yield return new object[] { "P4" };
            yield return new object[] { "P5" };
            yield return new object[] { "P6" };
            yield return new object[] { "P7" };
            yield return new object[] { "P8" };
            yield return new object[] { "P9" };
            yield return new object[] { "P10" };
            yield return new object[] { "P11" };
            yield return new object[] { "P12" };
            yield return new object[] { "P13" };
            yield return new object[] { "P14" };
            yield return new object[] { "P15" };
            yield return new object[] { "P16" };
            yield return new object[] { "P17" };
            yield return new object[] { "P18" };
            yield return new object[] { "P19" };
            yield return new object[] { "P20" };
            yield return new object[] { "P21" };
            yield return new object[] { "P22" };
            yield return new object[] { "P23" };
            yield return new object[] { "P24" };
            yield return new object[] { "P25" };
        }

        IEnumerator IEnumerable.GetEnumerator() => GetEnumerator();
    }

}
