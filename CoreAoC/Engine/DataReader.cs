using CoreAoC.Entities;
using CoreAoC.Interfaces;
using CoreAoC.Utils;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;

namespace CoreAoC.Engine
{
    internal class DataReader : IInputReader, IOutputReader
    {
        private const string _inputsPath = @".\Resources\{0}";
        private const string _resultsPath = @".\Resources\{0}\output.json";


        public IDictionary<int, IDictionary<Problem, IEnumerable<string>>> GetInputs()
            => RetrieveTextData(string.Format(_inputsPath, "Inputs"));

        public IDictionary<int, IDictionary<Problem, IEnumerable<string>>> GetSamples()
            => RetrieveTextData(string.Format(_inputsPath, "Samples"));


        public IDictionary<int, IDictionary<Problem, Tuple<Result, Result>>> GetInputResults()
            => RetrieveJsonData(string.Format(_resultsPath, "Inputs"));

        public IDictionary<int, IDictionary<Problem, Tuple<Result, Result>>> GetSampleResults()
            => RetrieveJsonData(string.Format(_resultsPath, "Samples"));


        private static IDictionary<int, IDictionary<Problem, IEnumerable<string>>> RetrieveTextData(string inputsPath)
        {
            IDictionary<int, IDictionary<Problem, IEnumerable<string>>> result = new Dictionary<int, IDictionary<Problem, IEnumerable<string>>>();

            IDictionary<int, IEnumerable<FileInfo>> inputInfo = FileTextExplorer.GetInputInfo(inputsPath);
            foreach (KeyValuePair<int, IEnumerable<FileInfo>> kvp in inputInfo)
                result.Add(kvp.Key, FileTextExplorer.ReadData(kvp.Value));

            return result;
        }

        private static IDictionary<int, IDictionary<Problem, Tuple<Result, Result>>> RetrieveJsonData(string path)
        {
            JObject rawSolutions = JObject.Parse(File.ReadAllText(path))!;

            IDictionary<int, IDictionary<Problem, Tuple<Result, Result>>> solutions = rawSolutions
                .ToObject<IDictionary<int, IDictionary<Problem, Tuple<Result, Result>>>>(GetConvertersSerializer())!;

            return solutions;
        }

        private static JsonSerializer GetConvertersSerializer()
        {
            JsonSerializer serializer = new();

            serializer.Converters.Add(new CalendarDeserializer());
            serializer.Converters.Add(new ResultsDeserializer());

            return serializer;
        }
    }
}
