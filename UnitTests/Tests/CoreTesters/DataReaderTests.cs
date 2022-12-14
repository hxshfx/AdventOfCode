using CoreAoC.Entities;
using CoreAoC.Factories.Implementation;
using CoreAoC.Factories.Interfaces;
using System.Globalization;
using System.Reflection;
using TestingProject.Utils;
using Xunit;

namespace TestingProject.Tests.CoreTesters
{
    public class DataReaderTests : CoreTesters<IDataReaderFactory>
    {
        public DataReaderTests()
            : base(new AppHostFactory()) { }


        #region IInputReaderTests

        [Fact]
        public void TestCreateInputReader()
            => Assert.NotNull(_service.CreateInputReader());


        [Fact]
        public void TestGetInputs()
            => AssertInputReader(_service.CreateInputReader().GetInputs());

        [Fact]
        public void TestGetSamples()
            => AssertInputReader(_service.CreateInputReader().GetSamples());

        #endregion

        #region IOutputReaderTests

        [Fact]
        public void TestCreateOutputReader()
            => Assert.NotNull(_service.CreateOutputReader());


        [Fact]
        public void TestGetInputResults()
            => AssertOutputReader(_service.CreateOutputReader().GetInputResults());

        [Fact]
        public void TestGetSampleResults()
            => AssertOutputReader(_service.CreateOutputReader().GetSampleResults());

        #endregion


        private static void AssertInputReader(IDictionary<int, IDictionary<Problem, IEnumerable<string>>> samples)
            => Assert.Multiple(
                () => Assert.NotNull(samples),
                () => Assert.NotEmpty(samples),

                () => Assert.All(samples.Keys, key
                   => Assert.IsAssignableFrom<DateTime>(DateTime.ParseExact(key.ToString(), "yyyy", CultureInfo.InvariantCulture))),

                () => Assert.All(samples.Values, dict =>
                      Assert.Multiple(
                        () => Assert.NotNull(dict),
                        () => Assert.NotEmpty(dict),

                        () => Assert.All(dict, kvp =>
                              Assert.Multiple(
                                () => Assert.NotNull(kvp.Key),
                                () => Assert.IsAssignableFrom<Problem>(kvp.Key),

                                () => Assert.NotNull(kvp.Key.Parts),
                                () => Assert.Contains(kvp.Key.Parts.Item1.GetType(), kvp.Key.GetType().GetNestedTypes(BindingFlags.NonPublic)),
                                () => Assert.Contains(kvp.Key.Parts.Item2.GetType(), kvp.Key.GetType().GetNestedTypes(BindingFlags.NonPublic)),

                                () => Assert.NotNull(kvp.Value),
                                () => Assert.NotEmpty(kvp.Value),
                                () => Assert.IsAssignableFrom<IEnumerable<string>>(kvp.Value)
                            )
                        )
                    )
                )
            );

        private static void AssertOutputReader(IDictionary<int, IDictionary<Problem, Tuple<Result, Result>>> samples)
            => Assert.Multiple(
                () => Assert.NotNull(samples),
                () => Assert.NotEmpty(samples),

                () => Assert.All(samples.Keys, key
                   => Assert.IsAssignableFrom<DateTime>(DateTime.ParseExact(key.ToString(), "yyyy", CultureInfo.InvariantCulture))),

                () => Assert.All(samples.Values, dict =>
                      Assert.Multiple(
                        () => Assert.NotNull(dict),
                        () => Assert.NotEmpty(dict),

                        () => Assert.All(dict, kvp =>
                              Assert.Multiple(
                                () => Assert.NotNull(kvp.Key),
                                () => Assert.IsAssignableFrom<Problem>(kvp.Key),

                                () => Assert.NotNull(kvp.Key.Parts),
                                () => Assert.Contains(kvp.Key.Parts.Item1.GetType(), kvp.Key.GetType().GetNestedTypes(BindingFlags.NonPublic)),
                                () => Assert.Contains(kvp.Key.Parts.Item2.GetType(), kvp.Key.GetType().GetNestedTypes(BindingFlags.NonPublic)),

                                () => Assert.NotNull(kvp.Value),
                                () => Assert.NotNull(kvp.Value.Item1),
                                () => Assert.NotNull(kvp.Value.Item2),
                                () => Assert.IsAssignableFrom<Result>(kvp.Value.Item1),
                                () => Assert.IsAssignableFrom<Result>(kvp.Value.Item2),
                                () => Assert.False(string.IsNullOrEmpty(kvp.Value.Item1.Answer)),
                                () => Assert.False(string.IsNullOrEmpty(kvp.Value.Item2.Answer))
                            )
                        )
                    )
                )
            );
    }
}
