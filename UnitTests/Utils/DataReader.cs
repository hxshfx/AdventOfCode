using Newtonsoft.Json.Linq;

namespace UnitTests.Utils
{
    public static class DataReader
    {
        private const string _samplesPath = @".\Samples\Y{0}\{1}.txt";
        private const string _solutionsPath = @".\Samples\solutions.json";


        public static IEnumerable<string> ReadSample(int year, string problem)
            => File.ReadLines(string.Format(_samplesPath, year, problem));

        public static IDictionary<string, object> ReadSolutions(int year)
            => JObject.Parse(File.ReadAllText(_solutionsPath))[year.ToString()]!
                .ToObject<IDictionary<string, object>>()!;
    }
}
