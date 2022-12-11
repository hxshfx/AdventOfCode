using CoreAoC.Entities;
using CoreAoC.Factories.Implementation;
using CoreAoC.Factories.Interfaces;
using CoreAoC.Interfaces;
using System.Globalization;
using TestingProject.Utils;
using Xunit;

namespace TestingProject.Tests.CoreTesters
{
    public class DataReaderFactoryTests : CoreTesters<IDataReaderFactory>
    {
        public DataReaderFactoryTests()
            : base(new AppHostFactory()) { }


        [Fact]
        public void TestCreateInputReader()
            => Assert.NotNull(_service.CreateInputReader());

        [Fact]
        public void TestInputReaderGetSamples()
        {
            IInputReader inputReader = _service.CreateInputReader();
            IDictionary<int, IDictionary<Problem, IEnumerable<string>>> samples = inputReader.GetSamples();

            Assert.Multiple(
                () => Assert.NotNull(samples),
                () => Assert.NotEmpty(samples),
                () => Assert.All(samples.Keys, key => Assert.IsAssignableFrom<DateTime>(DateTime.ParseExact(key.ToString(), "yyyy", CultureInfo.InvariantCulture))),
                () => Assert.All(samples.Values, dict => Assert.NotEmpty(dict)),
                () => Assert.All(samples.Values, dict => Assert.All(dict, kvp => Assert.NotNull(kvp.Key))),
                () => Assert.All(samples.Values, dict => Assert.All(dict, kvp => Assert.NotEmpty(kvp.Value)))
            );
        }
    }
}
