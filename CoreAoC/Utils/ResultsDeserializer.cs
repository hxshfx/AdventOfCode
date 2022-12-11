using CoreAoC.Entities;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;

namespace CoreAoC.Utils
{
    internal class ResultsDeserializer : JsonConverter<IDictionary<Problem, Tuple<Result, Result>>>
    {
        public override IDictionary<Problem, Tuple<Result, Result>>? ReadJson(JsonReader reader, Type objectType, IDictionary<Problem, Tuple<Result, Result>>? existingValue, bool hasExistingValue, JsonSerializer serializer)
        {
            JToken token = JToken.ReadFrom(reader);
            if (token.Type == JTokenType.Null)
                return new Dictionary<Problem, Tuple<Result, Result>>();

            IEnumerable<Type> yearProblems = AssemblySearcher.GetProblemsFromYear(int.Parse(reader.Path));

            IDictionary<Problem, Tuple<Result, Result>> result = new Dictionary<Problem, Tuple<Result, Result>>();
            foreach (JToken subToken in token.Children())
            {
                Type? problemType;
                if ((problemType = yearProblems.SingleOrDefault(t => t.Name.Equals(((JProperty)subToken).Name))) == null)
                    continue;

                Problem problem = (Problem)Activator.CreateInstance(problemType)!;
                Tuple<Result, Result> results = AssemblySearcher.GetResultsFromProblem((JObject)subToken.Children().Single());

                result.Add(problem, results);
            }

            return result;
        }

        public override void WriteJson(JsonWriter writer, IDictionary<Problem, Tuple<Result, Result>>? value, JsonSerializer serializer)
            => throw new NotImplementedException("Class for read-only");
    }
}
