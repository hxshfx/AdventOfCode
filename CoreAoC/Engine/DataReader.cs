using Newtonsoft.Json.Linq;

namespace CoreAoC.Engine
{
    public static class DataReader
    {
        private const string _inputsPath = @".\Resources\Inputs\Y{0}\P{1}.txt";
        private const string _solutionsPath = @".\Resources\solutions.json";


        public static IEnumerable<string> ReadInput(int year, int problem)
            => File.ReadLines(string.Format(_inputsPath, year, problem));

        public static IDictionary<string, object> ReadSolutions(int year)
            => JObject.Parse(File.ReadAllText(_solutionsPath))[year.ToString()]!
                .ToObject<IDictionary<string, object>>()!;
    }
}
